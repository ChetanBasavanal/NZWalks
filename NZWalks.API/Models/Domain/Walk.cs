namespace NZWalks.API.Models.Domain
{
    public class Walk
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKM { get; set; }
        public string? WalkImageURL { get; set; }
        public Guid DifficultyID { get; set; }
        public Guid RegionID { get; set; }

        //Navigation Property                       Inorder to build a connection btwn the models and (primary and foreign key) we do like this

        public Difficulty Difficulty { get; set; }
        public RegionDomain Region { get; set; }
    }
}
