using System.ServiceProcess;

namespace FaceFirst_AutoImageLoad
{
    internal static class Program
    {
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new FaceFirstAutoImageLoad()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
