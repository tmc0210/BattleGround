public class KeyWord
{
    private KeyWordType keyWord;

    public enum KeyWordType
    {
        Taunt,
        DivineShield,
        Poisonous,
        Windfury,
        MegaWindfury,
        Stealth
    }

    public KeyWord(KeyWordType keyWord)
    {
        this.keyWord = keyWord;
    }

    public bool IsKeyWord(KeyWordType keyWord)
    {
        return  this.keyWord == keyWord;
    }
}
