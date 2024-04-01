using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gym.Migrations
{
    public partial class initialmigrationmm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GymTraineeDetails_MonthlyFeeVouchers_MonthlyFeeVoucherID",
                table: "GymTraineeDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_GymTraineeDetails_Trainees_TraineeId",
                table: "GymTraineeDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_GymTraineeDetails_TrainingLevels_TrainingLevelID",
                table: "GymTraineeDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_GymTraineeDetails_TrainingLevels_TrainingLevelID1",
                table: "GymTraineeDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GymTraineeDetails",
                table: "GymTraineeDetails");

            migrationBuilder.RenameTable(
                name: "GymTraineeDetails",
                newName: "GymTraineeDetailViewModel");

            migrationBuilder.RenameIndex(
                name: "IX_GymTraineeDetails_TrainingLevelID1",
                table: "GymTraineeDetailViewModel",
                newName: "IX_GymTraineeDetailViewModel_TrainingLevelID1");

            migrationBuilder.RenameIndex(
                name: "IX_GymTraineeDetails_TrainingLevelID",
                table: "GymTraineeDetailViewModel",
                newName: "IX_GymTraineeDetailViewModel_TrainingLevelID");

            migrationBuilder.RenameIndex(
                name: "IX_GymTraineeDetails_TraineeId",
                table: "GymTraineeDetailViewModel",
                newName: "IX_GymTraineeDetailViewModel_TraineeId");

            migrationBuilder.RenameIndex(
                name: "IX_GymTraineeDetails_MonthlyFeeVoucherID",
                table: "GymTraineeDetailViewModel",
                newName: "IX_GymTraineeDetailViewModel_MonthlyFeeVoucherID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GymTraineeDetailViewModel",
                table: "GymTraineeDetailViewModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GymTraineeDetailViewModel_MonthlyFeeVouchers_MonthlyFeeVoucherID",
                table: "GymTraineeDetailViewModel",
                column: "MonthlyFeeVoucherID",
                principalTable: "MonthlyFeeVouchers",
                principalColumn: "MonthlyFeeVoucherID");

            migrationBuilder.AddForeignKey(
                name: "FK_GymTraineeDetailViewModel_Trainees_TraineeId",
                table: "GymTraineeDetailViewModel",
                column: "TraineeId",
                principalTable: "Trainees",
                principalColumn: "TraineeId");

            migrationBuilder.AddForeignKey(
                name: "FK_GymTraineeDetailViewModel_TrainingLevels_TrainingLevelID",
                table: "GymTraineeDetailViewModel",
                column: "TrainingLevelID",
                principalTable: "TrainingLevels",
                principalColumn: "TrainingLevelID");

            migrationBuilder.AddForeignKey(
                name: "FK_GymTraineeDetailViewModel_TrainingLevels_TrainingLevelID1",
                table: "GymTraineeDetailViewModel",
                column: "TrainingLevelID1",
                principalTable: "TrainingLevels",
                principalColumn: "TrainingLevelID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GymTraineeDetailViewModel_MonthlyFeeVouchers_MonthlyFeeVoucherID",
                table: "GymTraineeDetailViewModel");

            migrationBuilder.DropForeignKey(
                name: "FK_GymTraineeDetailViewModel_Trainees_TraineeId",
                table: "GymTraineeDetailViewModel");

            migrationBuilder.DropForeignKey(
                name: "FK_GymTraineeDetailViewModel_TrainingLevels_TrainingLevelID",
                table: "GymTraineeDetailViewModel");

            migrationBuilder.DropForeignKey(
                name: "FK_GymTraineeDetailViewModel_TrainingLevels_TrainingLevelID1",
                table: "GymTraineeDetailViewModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GymTraineeDetailViewModel",
                table: "GymTraineeDetailViewModel");

            migrationBuilder.RenameTable(
                name: "GymTraineeDetailViewModel",
                newName: "GymTraineeDetails");

            migrationBuilder.RenameIndex(
                name: "IX_GymTraineeDetailViewModel_TrainingLevelID1",
                table: "GymTraineeDetails",
                newName: "IX_GymTraineeDetails_TrainingLevelID1");

            migrationBuilder.RenameIndex(
                name: "IX_GymTraineeDetailViewModel_TrainingLevelID",
                table: "GymTraineeDetails",
                newName: "IX_GymTraineeDetails_TrainingLevelID");

            migrationBuilder.RenameIndex(
                name: "IX_GymTraineeDetailViewModel_TraineeId",
                table: "GymTraineeDetails",
                newName: "IX_GymTraineeDetails_TraineeId");

            migrationBuilder.RenameIndex(
                name: "IX_GymTraineeDetailViewModel_MonthlyFeeVoucherID",
                table: "GymTraineeDetails",
                newName: "IX_GymTraineeDetails_MonthlyFeeVoucherID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GymTraineeDetails",
                table: "GymTraineeDetails",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GymTraineeDetails_MonthlyFeeVouchers_MonthlyFeeVoucherID",
                table: "GymTraineeDetails",
                column: "MonthlyFeeVoucherID",
                principalTable: "MonthlyFeeVouchers",
                principalColumn: "MonthlyFeeVoucherID");

            migrationBuilder.AddForeignKey(
                name: "FK_GymTraineeDetails_Trainees_TraineeId",
                table: "GymTraineeDetails",
                column: "TraineeId",
                principalTable: "Trainees",
                principalColumn: "TraineeId");

            migrationBuilder.AddForeignKey(
                name: "FK_GymTraineeDetails_TrainingLevels_TrainingLevelID",
                table: "GymTraineeDetails",
                column: "TrainingLevelID",
                principalTable: "TrainingLevels",
                principalColumn: "TrainingLevelID");

            migrationBuilder.AddForeignKey(
                name: "FK_GymTraineeDetails_TrainingLevels_TrainingLevelID1",
                table: "GymTraineeDetails",
                column: "TrainingLevelID1",
                principalTable: "TrainingLevels",
                principalColumn: "TrainingLevelID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
