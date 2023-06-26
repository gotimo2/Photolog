namespace Photolog.Helpers
{
    public static class PermissionManager
    {
        public async static Task<bool> EnsurePermission<TPermission>() where TPermission : Permissions.BasePermission, new()
        {
            var permissionLevel = await Permissions.CheckStatusAsync<TPermission>();


            if (permissionLevel != PermissionStatus.Granted)
            {
                await Permissions.RequestAsync<TPermission>();
                permissionLevel = await Permissions.CheckStatusAsync<TPermission>();
            }

            if (permissionLevel == PermissionStatus.Denied || permissionLevel == PermissionStatus.Disabled)
            {
                return false;
            }

            if (permissionLevel == PermissionStatus.Granted)
            {
                return true;
            }

            return false;
        }

    }
}
