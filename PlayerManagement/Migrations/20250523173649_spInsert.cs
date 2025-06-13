using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlayerManagement.Migrations
{
    /// <inheritdoc />
    public partial class spInsert : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE TYPE FParamSkillType AS TABLE(
                    SkillName NVARCHAR(50),
                    SkillLevel NVARCHAR(50));
                    GO
                    CREATE OR ALTER PROCEDURE dbo.spInsertPlayer
                    @PlayerName NVARCHAR(50),
                    @MobileNo NVARCHAR(15),
                    @Email NVARCHAR(50),
                    @IsOverseas BIT,
                    @CitizenshipId INT,
                    @SigningFee DECIMAL,
                    @SigningDate DATETIME,
                    @ImageUrl NVARCHAR(100),
                    @PlayerSkills FParamSkillType READONLY
                    AS
                    BEGIN
                    BEGIN TRY
                    DECLARE @LocalSkills TABLE(
                    SkillName NVARCHAR(50),
                    SkillLevel NVARCHAR(50),
                    PlayerId INT)
                    DECLARE @PlayerId INT
                    INSERT INTO dbo.Players(PlayerName,MobileNo,Email,IsOverseas,CitizenshipId,SigningFee,SigningDate,ImageUrl) 
                    VALUES(@PlayerName,@MobileNo,@Email,@IsOverseas,@CitizenshipId,@SigningFee,@SigningDate,@ImageUrl);

                    SET @PlayerId = SCOPE_IDENTITY();
                    INSERT INTO @LocalSkills(SkillName,SkillLevel,PlayerId)
                    SELECT SkillName,SkillLevel,@PlayerId
                    FROM @PlayerSkills

                    INSERT INTO dbo.PlayerSkills(SkillName,SkillLevel,PlayerId)
                    SELECT SkillName,SkillLevel,@PlayerId
                    FROM @LocalSkills

                    END TRY
                    BEGIN CATCH
                    Throw
                    END CATCH
                    END
                    ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCUDURE IF EXISTS dbo.spInsertPlayer");
            migrationBuilder.Sql("DROP TYPE IF EXISTS ParamSkillType");
        }
    }
}
