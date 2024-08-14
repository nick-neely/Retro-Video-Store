using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RetroVideoStore.Migrations
{
    /// <inheritdoc />
    public partial class SeedVhsMovies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Title", "Description", "Format" },
                values: new object[,]
                {
                    { 11, "The Lion King", "Lion prince Simba and his father are targeted by his bitter uncle, who wants to ascend the throne himself.", "VHS" },
                    { 12, "Jurassic Park", "A pragmatic paleontologist visiting an almost complete theme park is tasked with protecting a couple of kids after a power failure causes the park's cloned dinosaurs to run loose.", "VHS" },
                    { 13, "Titanic", "A seventeen-year-old aristocrat falls in love with a kind but poor artist aboard the luxurious, ill-fated R.M.S. Titanic.", "VHS" },
                    { 14, "Star Wars: Episode IV - A New Hope", "Luke Skywalker joins forces with a Jedi Knight, a cocky pilot, a Wookiee, and two droids to save the galaxy from the Empire's world-destroying battle station, while also attempting to rescue Princess Leia from the mysterious Darth Vader.", "VHS" },
                    { 15, "Back to the Future", "Marty McFly, a 17-year-old high school student, is accidentally sent thirty years into the past in a time-traveling DeLorean invented by his close friend, the eccentric scientist Doc Brown.", "VHS" },
                    { 16, "The Terminator", "A human soldier is sent from 2029 to 1984 to stop an almost indestructible cyborg killing machine, sent from the same year, which has been programmed to execute a young woman whose unborn son is the key to humanity's future salvation.", "VHS" },
                    { 17, "Ghostbusters", "Three former parapsychology professors set up shop as a unique ghost removal service.", "VHS" },
                    { 18, "Indiana Jones and the Last Crusade", "In 1938, after his father goes missing while pursuing the Holy Grail, Indiana Jones finds himself up against the Nazis again to stop them from obtaining its powers.", "VHS" },
                    { 19, "E.T. the Extra-Terrestrial", "A troubled child summons the courage to help a friendly alien escape Earth and return to his home world.", "VHS" },
                    { 20, "Rocky", "A small-time boxer gets a supremely rare chance to fight a heavyweight champion in a bout in which he strives to go the distance for his self-respect.", "VHS" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValues: new object[] { 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 });
        }
    }
}
