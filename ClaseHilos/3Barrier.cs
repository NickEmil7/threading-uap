namespace ClaseHilos
{
    internal class _3Barrier
    {
        static Barrier barrera = new Barrier(2, (b) =>
        {
            Console.WriteLine($"Post-Phase action: {b.CurrentPhaseNumber}");
        });
        public static Barrier barrera_
        {
            get { return barrera; }
        }
        static void HolaUAP()
        {
            Console.WriteLine($"Proceso {Thread.CurrentThread.Name}");
            Thread.Sleep(4000);
            barrera.SignalAndWait();
            Console.WriteLine("Hola desde " + Thread.CurrentThread.Name);
        }
        

        internal static void Excecute()
        {
            Thread task1 = new Thread(HolaUAP);
            task1.Name = "Hilo 1";

            Thread task2 = new Thread(HolaUAP);
            task2.Name = "Hilo 2";

            Thread task3 = new Thread(() => Console.WriteLine("InlineAction"));

            //Excecute all tasks
            task1.Start();
            task2.Start();
            task3.Start();

            Console.WriteLine("Hola desde el hilo principal");
            Console.ReadLine();
        }
    }
}
