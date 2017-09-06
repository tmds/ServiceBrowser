using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using systemd1.DBus;
using Tmds.DBus;

namespace ServiceBrowser.Controllers
{
    [Route("/api/unit")]
    public class UnitController : Controller
    {
        [HttpPost("{id}/start")]
        public async Task<IActionResult> Start(string id)
        {
            try
            {
                IManager manager = Connection.System.CreateProxy<IManager>(Systemd.Service, Systemd.RootPath);
                await manager.StartUnitAsync(id, "replace");
                return new OkResult();
            }
            catch (Exception e)
            {
                return ExceptionResult(e);
            }
        }

        [HttpPost("{id}/stop")]
        public async Task<IActionResult> Stop(string id)
        {
            try
            {
                IManager manager = Connection.System.CreateProxy<IManager>(Systemd.Service, Systemd.RootPath);
                await manager.StopUnitAsync(id, "replace");
                return new NoContentResult();
            }
            catch (Exception e)
            {
                return ExceptionResult(e);
            }
        }

        [HttpGet("services")]
        public async Task<IActionResult> GetServices()
        {
            try
            {
                IManager manager = Connection.System.CreateProxy<IManager>(Systemd.Service, Systemd.RootPath);
                var services = new List<Service>();
                foreach (var unitInfo in await manager.ListUnitsAsync())
                {
                    if (unitInfo.Name.EndsWith(".service"))
                    {
                        string shortName = unitInfo.Name.Substring(0, unitInfo.Name.Length - ".service".Length);
                        IUnit unit = Connection.System.CreateProxy<IUnit>(Systemd.Service, unitInfo.Path);
                        string unitFileState = await unit.GetUnitFileStateAsync();
                        services.Add(
                            new Service {
                                Id = unitInfo.Name,
                                Name = shortName,
                                Description = unitInfo.Description,
                                Status = unitInfo.ActiveState,
                                Startup = unitFileState
                            }
                        );
                    }
                }
                foreach (var unitFile in await manager.ListUnitFilesAsync())
                {
                    if (unitFile.path.EndsWith(".service"))
                    {
                        string filename = Path.GetFileName(unitFile.path);
                        string shortName = filename.Substring(0, filename.Length - ".service".Length);
                        if (services.Any(_ => _.Name == shortName))
                        {
                            continue;
                        }
                        services.Add(
                            new Service {
                                Id = filename,
                                Name = shortName,
                                Description = "(Not loaded)",
                                Status = ActiveState.Inactive,
                                Startup = unitFile.state
                            }
                        );
                    }
                }
                services.Sort((x, y) => x.Name.CompareTo(y.Name));
                return new ObjectResult(services);
            }
            catch (Exception e)
            {
                return ExceptionResult(e);
            }
        }

        private IActionResult ExceptionResult(Exception e)
        {
            Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return new JsonResult(new { message = e.Message });
        }

        private class Service
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string Status { get; set; }
            public string Startup { get; set; }
        }
    }
}