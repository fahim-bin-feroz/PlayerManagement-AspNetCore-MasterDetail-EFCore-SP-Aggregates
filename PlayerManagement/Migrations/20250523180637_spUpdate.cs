using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlayerManagement.Migrations
{
    /// <inheritdoc />
    public partial class spUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE TYPE EditParamSkillType AS TABLE(
             SkillName NVARCHAR(50),
             SkillLevel NVARCHAR(50)
         );
        GO
        CREATE OR ALTER PROCEDURE dbo.spUpdatePlayer

         @PlayerName NVARCHAR(50),
         @MobileNo NVARCHAR(15),
         @Email NVARCHAR(50),
         @IsOverseas BIT,
         @CitizenshipId INT,
         @SigningFee DECIMAL,
         @SigningDate DATETIME,
         @ImageUrl NVARCHAR(100),
         @PlayerSkills EditParamSkillType READONLY,
         @PlayerId INT
         AS
         BEGIN
         BEGIN TRY
         DECLARE @LocalSkills TABLE(
            SkillName NVARCHAR(50),
            SkillLevel NVARCHAR(50),
            PlayerId INT
          );
 
        UPDATE dbo.Players
        SET
             PlayerName = @PlayerName,
             MobileNo = @MobileNo,
             Email = @Email,
             IsOverseas = @IsOverseas,
             CitizenshipId = @CitizenshipId,
             SigningFee = @SigningFee,
             SigningDate = @SigningDate,
             ImageUrl = @ImageUrl
        WHERE
        PlayerId = @PlayerId;
        DELETE FROM dbo.PlayerSkills
        WHERE PlayerId = @PlayerId;

        INSERT INTO dbo.PlayerSkills(SkillName, SkillLevel, PlayerId)
        SELECT SkillName, SkillLevel, @PlayerId
        FROM @PlayerSkills;

        END TRY
        BEGIN CATCH
        Throw;
        END CATCH
        END
     ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCUDURE IF EXISTS dbo.spUpdatePlayer");
            migrationBuilder.Sql("DROP TYPE IF EXISTS EditParamSkillType");
        }
    }
}
