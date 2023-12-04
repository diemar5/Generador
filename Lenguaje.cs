using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Generador
{
    public class Lenguaje : Sintaxis
    {
        public Lenguaje()
        {
        }
        public Lenguaje(string nombre) : base(nombre)
        {
        }
        public void generaLenguaje()
        {

            generado.WriteLine("using System;");
            generado.WriteLine("using System.Collections.Generic;");
            generado.WriteLine("using System.Linq;");
            generado.WriteLine("using System.Reflection.PortableExecutable;");
            generado.WriteLine("using System.Threading.Tasks;");
            generado.WriteLine();
            generado.WriteLine("namespace Generado");
            generado.WriteLine("{");
            generado.WriteLine("    public class Lenguaje : Sintaxis");
            generado.WriteLine("    {");
            generado.WriteLine("        public Lenguaje()");
            generado.WriteLine("        {");

            generado.WriteLine("        }");
            generado.WriteLine("        public Lenguaje(string nombre) : base(nombre)");
            generado.WriteLine("        {");
            generado.WriteLine("        }");

            Producciones();

            generado.WriteLine("    }");
            generado.WriteLine("}");
        }
        private void Producciones()
        {
            generado.WriteLine("        public void " + getContenido() + "()");
            generado.WriteLine("        {");
            match(Tipos.SNT);
            match(Tipos.Flechita);
            listaSimbolos();
            match(Tipos.FinProduccion);
            generado.WriteLine("        }");
            if (getClasificacion() == Tipos.SNT)
            {
                Producciones();
            }
        }
        private void listaSimbolos()
        {
            if (esPalabraReservada(getContenido()))
            {
                generado.WriteLine("            match(Tipos." + getContenido() + ");");
                match(Tipos.SNT);
            }
            else if (getClasificacion() == Tipos.ST)
            {
                generado.WriteLine("            match(\"" + getContenido() + "\");");
                match(Tipos.ST);
            }
            else if (getClasificacion() == Tipos.SNT)
            {
                generado.WriteLine("            " + getContenido() + "();");
                match(Tipos.SNT);
            }
            else if (getClasificacion() == Tipos.Epsilon)
            {
                match(Tipos.Epsilon);
                match(Tipos.PIzq);
                string simbolo = getContenido();
                if (esPalabraReservada(simbolo))
                {
                    generado.WriteLine("            if (getClasificacion() == Tipos." + simbolo + ")");
                    generado.WriteLine("            {");
                    generado.WriteLine("                match(Tipos." + simbolo + ");");
                    match(Tipos.SNT);
                }
                else if (getClasificacion() == Tipos.ST)
                {
                    generado.WriteLine("            if (getContenido() == \"" + simbolo + "\")");
                    generado.WriteLine("            {");
                    generado.WriteLine("                match(\"" + simbolo + "\");");
                    match(Tipos.ST);
                }
                else
                {
                    throw new Error("No puedes comparar el método " + getContenido() + "() en la", log, linea, columna);
                }
                masSimbolosEps();
                match(Tipos.PDer);
                generado.WriteLine("            }");
            }
            if (getClasificacion() != Tipos.FinProduccion)
            {
                listaSimbolos();
            }
        }
        private void masSimbolosEps()
        {
            if (esPalabraReservada(getContenido()))
            {
                generado.WriteLine("                match(Tipos." + getContenido() + ");");
                match(Tipos.SNT);
            }
            else if (getClasificacion() == Tipos.ST)
            {
                generado.WriteLine("                match(\"" + getContenido() + "\");");
                match(Tipos.ST);
            }
            else if (getClasificacion() == Tipos.SNT)
            {
                generado.WriteLine("                " + getContenido() + "();");
                match(Tipos.SNT);
            }
            if (getClasificacion() != Tipos.PDer)
            {
                masSimbolosEps();
            }
        }
        private bool esPalabraReservada(string palabra)
        {
            switch (palabra)
            {
                case "Identificador":
                case "Numero":
                case "Asignacion":
                case "Inicializacion":
                case "OperadorRelacional":
                case "OperadorTermino":
                case "OperadorFactor":
                case "IncrementoTermino":
                case "IncrementoFactor":
                case "Cadena":
                case "Ternario":
                case "FinSentencia":
                case "OperadorLogico":
                case "Inicio":
                case "Fin":
                case "Caracter":
                case "TipoDato":
                case "Zona":
                case "Condicion":
                case "Ciclo":
                    return true;
            }
            return false;
        }
        /*
            
        */
    }
}