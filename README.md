# ServiceBrowser demo

This demo application shows and allows control of [systemd](https://www.freedesktop.org/wiki/Software/systemd/) services.
Interfacing with systemd is done via the [systemd D-Bus API](https://www.freedesktop.org/wiki/Software/systemd/dbus/).

This demo application makes use of the following libraries:
* [ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/): .NET Core application web framework
* [Tmds.DBus](https://github.com/tmds/Tmds.DBus): D-Bus bindings for .NET Core
* [Knockout](http://knockoutjs.com/): Javascript UI databinding framework

To allow control of services, the user must have appropriate permissions. Even then, default configuration requires users to authorize as administrator to perform these operations. To allow control to users of the `wheel` group without authentication, create/append the following to `/etc/polkit-1/rules.d/00-early-checks.rules`:

```js
/* Allow users in wheel group to control units without authentication */
polkit.addRule(function(action, subject) {
    if (action.id == "org.freedesktop.systemd1.manage-units" &&
        subject.isInGroup("wheel")) {
        return polkit.Result.YES;
    }
});

```

**Security Considerations**

Anyone with access to the website will be able to see and control the services. Only run this on your personal machine on localhost.

## Running the demo

```
$ git clone https://github.com/tmds/ServiceBrowser.git
$ cd ServiceBrowser/src/ServiceBrowser/
$ dotnet run
```
Now open your browser at: `http://localhost:5000/`.