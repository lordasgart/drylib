using System;

namespace DryLib.IO
{
    public class Path
    {
        public static char ActiveWinDrive = 'C';
        public static string CustomRoot = string.Empty;
        public static PathReturnStyle PathReturnStyle = PathReturnStyle.Auto;

        private readonly string _path;
        private readonly bool _isPosixStyle;

        public Path(string path)
        {
            _path = path;
            _isPosixStyle = path.StartsWith("/");
        }

        public string Get()
        {
            if (ShouldGetPosixEnvironmentPathStyle())
            {
                if (_isPosixStyle) return _path;

                var posixStylePath = GetPosixPath(_path);
                return posixStylePath;
            }
            else
            {
                if (!_isPosixStyle) return _path;

                var winStylePath = GetWinPath(_path);
                return winStylePath;
            }
        }

        public static string Combine(Path p1, Path p2)
        {
            return System.IO.Path.Combine(p1.ToString(), p2.ToString());
        }

        private static string GetPosixPath(string winPath)
        {
            var posixPath = winPath.Replace("\\", "/");

            // PowerShell uses the driveLetter of the current drive
            var driveLetter = ActiveWinDrive;

            //also respect Mintty style /c/ and other possibilities for other ROOTs e.g. /Volumes/BOOTCAMP
            //this is done by now with the static CustomRoot
            posixPath = posixPath.Replace(driveLetter.ToString().ToUpper() + ":", CustomRoot).Replace(driveLetter.ToString().ToLower() + ":", CustomRoot);

            posixPath = posixPath.Replace(" ", "\\ "); //mask space with \

            return posixPath;
        }

        private static string GetWinPath(string posixPath)
        {
            var winStylePath = posixPath.Replace("/", "\\");

            winStylePath = $"{ActiveWinDrive}:" + winStylePath;

            return winStylePath;
        }

        private static bool ShouldGetPosixEnvironmentPathStyle()
        {
            switch (PathReturnStyle)
            {
                case PathReturnStyle.Windows:
                    return false;
                case PathReturnStyle.Posix:
                    return true;
                default:
                    return Environment.OSVersion.Platform == PlatformID.Unix ||
                           Environment.OSVersion.Platform == PlatformID.MacOSX;
            }
        }

        public override string ToString()
        {
            return Get();
        }
    }
}
