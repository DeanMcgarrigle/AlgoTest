using CsvHelper.Configuration;

namespace Utils
{
    sealed class ClassMap : CsvClassMap<DataModel>
    {
        public ClassMap()
        {
            Map(m => m.League).Name("Div");
            Map(m => m.HomeTeam).Name("HomeTeam");
            Map(m => m.AwayTeam).Name("AwayTeam");
            Map(m => m.DateTime).Name("Date");
        }
    }
}
