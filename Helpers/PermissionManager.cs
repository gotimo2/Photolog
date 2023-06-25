using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photolog.Helpers
{
    public static class PermissionManager
    {
        public async static Task<bool> GetCameraPermissions()
        {
            var permissionLevel = await Permissions.CheckStatusAsync<Permissions.Camera>();

            if (permissionLevel != PermissionStatus.Granted)
            {
                await Permissions.RequestAsync<Permissions.Camera>();

                permissionLevel = await Permissions.CheckStatusAsync<Permissions.Camera>();
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


        public async static Task<bool> GetStorageWritePermissions()
        {
            

            var permissionLevel = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();


            if (permissionLevel != PermissionStatus.Granted)
            {
                await Permissions.RequestAsync<Permissions.StorageWrite>();

               
                permissionLevel = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();
                Console.WriteLine("Permission status: " + permissionLevel);
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

        public async static Task<bool> GetStorageReadPermissions()
        {


            var permissionLevel = await Permissions.CheckStatusAsync<Permissions.StorageRead>();


            if (permissionLevel != PermissionStatus.Granted)
            {
                await Permissions.RequestAsync<Permissions.StorageRead>();


                permissionLevel = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
                Console.WriteLine("Permission status: " + permissionLevel);
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
