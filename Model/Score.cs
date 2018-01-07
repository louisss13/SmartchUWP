using System.Collections.Generic;

namespace Model
{
    public class Score
    {
        public List<EJoueurs> Joueurs { get; set; }
        public List<Dictionary<EJoueurs, int>> PointLevel { get; set; }
    }
}