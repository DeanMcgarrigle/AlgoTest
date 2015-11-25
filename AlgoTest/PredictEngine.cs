using DataAccess;
using Model;

namespace AlgoTest
{
    public class PredictEngine
    {


        public static string Predict(string home, string away)
        {
            var context = new AlgoTestContext();
            var leagueRepo = new LeagueRepository(context);

            var homeForm = "";
            var awayForm = "";

            var hometeam = leagueRepo.LoadHomeTeam(home);
            var awayteam = leagueRepo.LoadAwayTeam(away);


            #region Home Team (Home Form)

            foreach (var stats in hometeam)
            {
                var r = "";

                switch (stats.FTResult)
                {
                    case "H":
                        r = "W";
                        break;
                    case "A":
                        r = "L";
                        break;
                    default:
                        r = "D";
                        break;
                }

                homeForm = homeForm + r;
            }

            var homeTeamFormValue = 0;

            foreach (var c in homeForm)
            {
                if (c.Equals("W")) homeTeamFormValue = homeTeamFormValue + 3;
            }
          



            #endregion

            #region Away Team (Away Form)

            foreach (var stats in awayteam)
            {
                var r = "";
                switch (stats.FTResult)
                {
                    case "A":
                        r = "W";
                        break;
                    case "H":
                        r = "L";
                        break;
                    default:
                        r = "D";
                        break;
                }
                awayForm = awayForm + r;
            }

            #endregion

           

            var result = "some result";

            return result;
        }
    }
}
