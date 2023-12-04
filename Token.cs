namespace Generador
{
    public class Token
    {
        public enum Tipos { ST, SNT, Flechita, Epsilon, FinProduccion, Or,
                          PIzq, PDer };
        private string Contenido;
        private Tipos Clasificacion;

        public Token()
        {
            Contenido = "";
            Clasificacion = Tipos.ST;
        }
        public void setContenido(string Contenido)
        {
            this.Contenido = Contenido;
        }
        public void setClasificacion(Tipos Clasificacion)
        {
            this.Clasificacion = Clasificacion;
        }
        public string getContenido()
        {
            return this.Contenido;
        }
        public Tipos getClasificacion()
        {
            return this.Clasificacion;
        }
    }
}