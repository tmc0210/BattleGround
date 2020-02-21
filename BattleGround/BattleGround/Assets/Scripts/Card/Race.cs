public class Race
{
    private RaceType race;

    public enum RaceType
    {
        Murloc,
        Beast,
        Demon,
        Mech,
        Dragon,
        Element
    }

    public Race(RaceType race)
    {
        this.race = race;
    }

    public RaceType GetRace()
    {
        return race;
    }

    public bool IsRace(RaceType race)
    {
        return this.race == race;
    }
}
