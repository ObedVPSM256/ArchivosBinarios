using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace ArchivosBinarios
{
    class ArchivoBinarioEmpleados
    {
        //declaración de flujos
        BinaryWriter bw = null; //flujo salida
        BinaryReader br = null; //flujo entrada

        //campos de la clase
        string Nombre, Direccion;
        long Tel;
        int NumEmp, DiasTrabajados;
        float SalarioDiario;

        public void CrearArchivos(string Archivo)
        {
            //variable local
            char resp;
            
            try
            {
                //creación del flujo para escribir datos en archivo
                bw = new BinaryWriter(new FileStream(Archivo, FileMode.Create, FileAccess.Write));
                //captura de datos
                do
                {
                    Console.Clear();
                    Console.Write("Numero del empleado: "); NumEmp = Int32.Parse(Console.ReadLine());
                    Console.Write("Nombre del empleado: "); Nombre = Console.ReadLine();
                    Console.Write("Direccion del empleado: "); Direccion = Console.ReadLine();
                    Console.Write("Teléfono del empleado: "); Tel = Int64.Parse(Console.ReadLine());
                    Console.Write("Días trabajados del empleado: "); DiasTrabajados = Int32.Parse(Console.ReadLine());
                    Console.Write("Salario diario del empleado: "); SalarioDiario = Single.Parse(Console.ReadLine());

                    //escribe los datos del archivo
                    bw.Write(NumEmp);
                    bw.Write(Nombre);
                    bw.Write(Direccion);
                    bw.Write(Tel);
                    bw.Write(DiasTrabajados);
                    bw.Write(SalarioDiario);

                    Console.Write("\n\nDeseas almacenar otro registro (s/n)?");
                    resp = Char.Parse(Console.ReadLine());
                }
                while ((resp == 's') || (resp == 'S'));
            }
            catch(IOException e)
            {
                Console.WriteLine("\nError: " + e.Message);
                Console.WriteLine("\nRuta: " + e.Message);
            }
            finally
            {
                if (bw != null) bw.Close(); //Cierra flujo
                Console.Write("\nPresione <enter> para terminar la escritura de datos y regresar al menú.");
                Console.ReadKey();
            }
        }
        public void MostrarArchivo (string Archivo)
        {
            try
            {
                //verifica si existe el archivo
                if (File.Exists(Archivo))
                {
                    //creación flujo para leer datos del archivo
                    br = new BinaryReader(new FileStream(Archivo, FileMode.Open, FileAccess.Read));
                    //despliegue de datos en pantalla
                    Console.Clear();
                    do
                    {
                        //lectura de registros mientras no llegue a EndOfFile
                        NumEmp = br.ReadInt32();
                        Nombre = br.ReadString();
                        Direccion = br.ReadString();
                        Tel = br.ReadInt64();
                        DiasTrabajados = br.ReadInt32();
                        SalarioDiario = br.ReadSingle();

                        //muestra los datos
                        Console.WriteLine("Numero del empleado: " + NumEmp);
                        Console.WriteLine("Nombre del empleado: " + Nombre);
                        Console.WriteLine("Direccion del empleado" + Direccion);
                        Console.WriteLine("Telefono del empleado" + Tel);
                        Console.WriteLine("Dias trabajados: " + DiasTrabajados);
                        Console.WriteLine("Salario diario: {0:C}" + SalarioDiario);
                        Console.WriteLine("SUELDO TOTAL del empleado: {0:C}", (DiasTrabajados * SalarioDiario));
                        Console.WriteLine("\n");
                    }
                    while (true);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("\n\nEl archivo" + Archivo + "No existe en el disco!!");
                    Console.Write("\nPresione <enter> para continuar...");
                    Console.ReadKey();
                }
            }
            catch (EndOfStreamException)
            {
                Console.WriteLine("\n\nFin del listado de empleados");
                Console.Write("\nPresione <enter> para continuar...");
                Console.ReadKey();
            }
            finally
            {
                if (br != null) br.Close(); //cierra flujo
                Console.Write("\nPresione <enter> para terminar la Lectura de Datos y regresar al Menu.");
                Console.ReadKey();
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //declaración de variables auxiliares
            string Arch = null;
            int opcion;

            //creación del objeto
            ArchivoBinarioEmpleados Al = new ArchivoBinarioEmpleados();

            //Menu de opciones
            do
            {
                Console.Clear();
                Console.WriteLine("\n*** ARCHIVO BINARIO EMPLEADOS ***");
                Console.WriteLine("1. Creación de un Archivo.");
                Console.WriteLine("2. Lectura de un Archivo.");
                Console.WriteLine("3. Salir del programa.");
                Console.Write("\nIntroduzca su opción a elegir: ");
                opcion = Int16.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        //Bloque de escritura
                        try
                        {
                            //captura nombre archivo
                            Console.Write("\nNombre del archivo a crear: ");
                            Arch = Console.ReadLine();

                            //verifica si existe el archivo
                            char resp = 's';
                            if (File.Exists(Arch))
                            {
                                Console.Write("\nEl archivo ya existe, deseas sobreescribirlo (s/n) ?");
                                resp = Char.Parse(Console.ReadLine());
                            }
                            if ((resp == 's') || (resp == 'S'))
                                Al.CrearArchivos(Arch);
                        }
                        catch (IOException e)
                        {
                            Console.WriteLine("\nError : " + e.Message);
                            Console.WriteLine("\nRuta : " + e.StackTrace);
                        }
                        break;

                    case 2:
                        //bloque de lectura
                        try
                        {
                            //captura nombre archivo
                            Console.Write("\nNombre del Archivo que desea Leer: ");
                            Arch = Console.ReadLine();
                            Al.MostrarArchivo(Arch);
                        }
                        catch (IOException e)
                        {
                            Console.WriteLine("\nError : " + e.Message);
                            Console.WriteLine("\nRuta : " + e.StackTrace);
                        }
                        break;

                    case 3:
                        Console.Write("\nPresione <enter> para Salir del Programa.");
                        Console.ReadKey();
                        break;

                    default:
                        Console.Write("\nLa opción elegida no existe, Presione <enter> para Continuar...");
                        Console.ReadKey();
                        break;
                }
            }
            while (opcion != 3);
        }
    }
}
