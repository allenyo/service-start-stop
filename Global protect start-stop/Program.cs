using System.ServiceProcess;

namespace ServiceControllerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    Console.Write("Service name - ");
                    var serviceName = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(serviceName))
                        throw new InvalidOperationException("service name empty");

                    Console.Write("action (start or stop) - ");
                    var action = Console.ReadLine();

                    if (action != "start" && action != "stop")
                        throw new InvalidOperationException("action need be start or stop");

                    ServiceController service = new ServiceController(serviceName);


                    if (action == "start")
                    {
                        if (service.Status != ServiceControllerStatus.Running && service.Status != ServiceControllerStatus.StartPending)
                        {
                            Console.WriteLine("Starting service...");
                            service.Start();
                            service.WaitForStatus(ServiceControllerStatus.Running);
                            Console.WriteLine("Service started successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Service is already running.");
                        }
                    }
                    else if (action == "stop")
                    {
                        if (service.Status != ServiceControllerStatus.Stopped && service.Status != ServiceControllerStatus.StopPending)
                        {
                            Console.WriteLine("Stopping service...");
                            service.Stop();
                            service.WaitForStatus(ServiceControllerStatus.Stopped);
                            Console.WriteLine("Service stopped successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Service is already stopped.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);

                }
            }       
        }
    }
}
