namespace StringMatch.Models
{
    public interface IPattern
    {
        int[] PrefixTable { get; set; }
        string Subtext { get; set; }
    }
}