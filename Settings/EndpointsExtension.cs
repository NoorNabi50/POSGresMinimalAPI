using POSGresApi.Abstraction;
using System.Reflection;

namespace POSGresApi.Settings
{
    public static class EndpointsExtension
    {

        public static void ConfigureEndPoints(this WebApplication app)
        {
            try
            {
                Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
                IEnumerable<Type> endPointRegisteredClasses = assemblies.SelectMany(x => x.GetTypes()).
                              Where(p => typeof(IRegisterEndPoints).IsAssignableFrom(p)
                              && !p.IsInterface && !p.IsAbstract);
                foreach (Type @class in endPointRegisteredClasses)
                {
                    var Instance = Activator.CreateInstance(@class) as IRegisterEndPoints;
                    Instance.RegisterEndPoints(app);
                }
            }  
            catch (Exception e)
            {
                Console.WriteLine("Exception in ConfigueEndPoints " + e.ToString);
            }


        }
    }
}
