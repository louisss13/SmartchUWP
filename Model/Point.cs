namespace Model
{
    public class Point
    {
        public long Id { get; set; }
        public EJoueurs Joueur { get; set; }
        public int Order { get; set; }
    }
}