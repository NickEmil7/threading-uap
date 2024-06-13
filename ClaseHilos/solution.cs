namespace ClaseHilos
{
   internal class Producto
   {
      public string Nombre { get; set; }
      public decimal PrecioUnitarioDolares { get; set; }
      public int CantidadEnStock { get; set; }

      public Producto(string nombre, decimal precioUnitario, int cantidadEnStock)
      {
         Nombre = nombre;
         PrecioUnitarioDolares = precioUnitario;
         CantidadEnStock = cantidadEnStock;
      }
   }
   internal class Solution //reference: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/lock
   {

      static List<Producto> productos = new List<Producto>
        {
            new Producto("Camisa", 10, 50),
            new Producto("Pantalón", 8, 30),
            new Producto("Zapatilla/Champión", 7, 20),
            new Producto("Campera", 25, 100),
            new Producto("Gorra", 16, 10)
        };

      static int precio_dolar = 500;
      static Mutex mutex = new Mutex();

      static Barrier barrera = new Barrier(3,(b) => {
         Console.WriteLine("Generando Informe...");
         Thread task3 = new Thread(Tarea3);
         task3.Name = "Informe";
         task3.Start(); 
         
      });
      public static Barrier _barrera{
         get{return barrera;}
      }
      static void Tarea1()
      {
         Console.WriteLine("Actualizando {0}...", Thread.CurrentThread.Name);

         mutex.WaitOne();
         Console.WriteLine("Modificando la lista de {0}...", Thread.CurrentThread.Name);

         foreach (Producto p in productos)
         {
            p.CantidadEnStock += 10;
         }
         Thread.Sleep(4000);
         Console.WriteLine("Actualizado correctamente");
         Console.WriteLine();

         mutex.ReleaseMutex();
         barrera.SignalAndWait();
         //throw new NotImplementedException();
      }
      static void Tarea2()
      {
         Console.WriteLine("Actualizando {0}...", Thread.CurrentThread.Name);
         precio_dolar = precio_dolar + 10;
         Thread.Sleep(4000);
         Console.WriteLine();
         //throw new NotImplementedException();
         barrera.SignalAndWait();
      }
      static void Tarea3()
      {
         Console.WriteLine("INFORME:");
         foreach (Producto p in productos)
         {
            Console.WriteLine("Producto: {0}, Cantidad: {1}, Precio Unitario en pesos: {2}",p.Nombre, p.CantidadEnStock, p.PrecioUnitarioDolares*precio_dolar);
         }
         //throw new NotImplementedException();
      }
      static void Tarea4(){
         Console.WriteLine("Ajustando los precios por {0}", Thread.CurrentThread.Name);
         Console.WriteLine();

         mutex.WaitOne();
         Console.WriteLine("Modificando la lista de precios...");

         foreach (Producto p in productos)
         {
            p.PrecioUnitarioDolares  = p.PrecioUnitarioDolares + p.PrecioUnitarioDolares/10;
         }

         Thread.Sleep(4000);
         
         Console.WriteLine("Actualizado correctamente");
         Console.WriteLine();

         mutex.ReleaseMutex();
         barrera.SignalAndWait();

      }

      internal static void Excecute()
      {
         //throw new NotImplementedException();
         Thread task1 = new Thread(Tarea1);
         task1.Name = "Stock";

         Thread task2 = new Thread(Tarea2);
         task2.Name = "Precio Dolar";

         Thread task4 = new Thread(Tarea4);
         task4.Name  = "Inflacion";

         task1.Start();
         task2.Start();
         task4.Start();
         
      }
   }
}