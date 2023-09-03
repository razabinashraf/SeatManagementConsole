namespace SeatManagementConsole
{
    public class StartPage
    {
        private readonly IEnumerable<IDoWork> _doWorkStrategy;
        public StartPage(IEnumerable<IDoWork> doWork)
        {
            _doWorkStrategy = doWork;
        }
        // Readability
        public void DoWork()
        {
            Console.WriteLine("\nPlease enter an option: " +
                   "\n1. Onboard a facility" +
                   "\n2. Onboard meeting room" +
                   "\n3. Onboard cabin" +
                   "\n4. Onboard seats" +
                   "\n5. Upload employee details" +
                   "\n6. Allocate seats" +
                   "\n0. EXIT");
            int choice = Convert.ToInt32(Console.ReadLine());
            var strategy = _doWorkStrategy.Where(_ => _.WorkType == choice).FirstOrDefault();
            strategy?.Do();
        }
    }
    

    public interface IDoWork
    {
        public int WorkType { get; }
        void Do();
    }
}