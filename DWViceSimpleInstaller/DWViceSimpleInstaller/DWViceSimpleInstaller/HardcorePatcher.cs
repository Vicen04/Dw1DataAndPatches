using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DWViceSimpleInstaller
{
    public partial class HardcorePatcher : Form
    {
        private OpenFileDialog openFileDialog;
        private string filePath = string.Empty;
        MainApp parentForm;
        Button[] patchTypes;
        Button[] patchTypesInfo;
        GroupBox[] patchesContainers;
        PatchConfirm tempConfirm;
        public HardcorePatcher(MainApp oldForm)
        {
            InitializeComponent();            
            parentForm = oldForm;
            parentForm.patchFinished += new EventHandler(EndPatchingApp);
            patchTypes = new Button[5] { GameplayPatches, DigimonPatches, BugFixes, UsefulPatches, TechPatches };
            patchTypesInfo = new Button[5] {InfoDifficulty, InfoDigimon, BugFixesInfo, InfoUseful, InfoTechniques};
            patchesContainers = new GroupBox[5] {usefulContainer, techContainer, bugFixesContainer, DifficultyPatchesContainer, DigimonGroup };
            this.FormClosed += new FormClosedEventHandler(parentForm.RestartBools);

            if (parentForm.GetPatchType() == MainApp.patchType.NONE)
            {
                this.HarderBosses.Enabled = true;
                this.HarderBosses.Visible = true;
                this.HarderBossesInfo.Enabled = true;
                this.HarderBossesInfo.Visible = true;
                this.bonusTryFix.Visible = true;
                this.bonusTryFix.Enabled = true;
                this.uBonustry.Visible = true;
                this.sBonusTry.Visible = true;
                this.Text = "Optional patches";
                PatchButton.BackColor = Color.Goldenrod;
            }
        }
        

        private void ViceHackPatcher_Load(object sender, EventArgs e)
        {
            openFileDialog = new OpenFileDialog()
            {
                Filter = "BIN files (*.BIN)|*.BIN| ISO files (*.ISO)|*.ISO",
                FileName = "Digimon World (USA).bin",
                Title = "Choose game file"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //Get the path of specified file
                filePath = openFileDialog.FileName;
            }
            else
            {
                this.Close();
            }
        }

        private void ChangeButtonColours(Button coloured)
        {
            foreach (Button button in patchTypes)
            {
                if (button != coloured)
                    button.BackColor = SystemColors.Control;
                else
                    button.BackColor = SystemColors.Highlight;
            }
        }

        private void EnableContainer(GroupBox container)
        {
            foreach (GroupBox patches in patchesContainers)
            {
                patches.Enabled = (patches == container);
                patches.Visible = (patches == container);
            }
        }

        private void EnableInfo(Button typeInfo)
        {
            foreach (Button info in patchTypesInfo)
            {
                info.Enabled = (info == typeInfo);
                info.Visible = (info == typeInfo);
            }
        }

        private void GameplayPatches_Click(object sender, EventArgs e)
        {
            ChangeButtonColours(GameplayPatches);
            EnableContainer(DifficultyPatchesContainer);
            EnableInfo(InfoDifficulty);            
        }

        private void DigimonPatches_Click(object sender, EventArgs e)
        {
            ChangeButtonColours(DigimonPatches);
            EnableContainer(DigimonGroup);
            EnableInfo(InfoDigimon);

        }

        private void BugFixes_Click(object sender, EventArgs e)
        {
            ChangeButtonColours(BugFixes);
            EnableContainer(bugFixesContainer);
            EnableInfo(BugFixesInfo);
        }

        private void UsefulPatches_Click(object sender, EventArgs e)
        {
            ChangeButtonColours(UsefulPatches);
            EnableContainer(usefulContainer);
            EnableInfo(InfoUseful);
        }

        private void TechPatches_Click(object sender, EventArgs e)
        {
            ChangeButtonColours(TechPatches);
            EnableContainer(techContainer);
            EnableInfo(InfoTechniques);
        }
       

        private void PatchButton_Click(object sender, EventArgs e)
        {
            parentForm.SetFilePath(filePath);
            tempConfirm = new PatchConfirm(parentForm);

            if (parentForm.GetPatchType() == MainApp.patchType.HARDCORE)
                tempConfirm.SetFileName("Digimon World Hardcore");
            else
                tempConfirm.SetFileName("Digimon World patched");
            tempConfirm.ShowDialog();
        }

       

        private void hardcoreBattler_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetSuperHardcore(hardcoreBattler.Checked);
        }

        private void FilthChanllenge_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetFilth(FilthChanllenge.Checked);
            if (FilthChanllenge.Checked)
            {
                restoreFilth.Enabled = true;
                UnlockAreasVice.Enabled = false;
                UnlockAreasVice.Checked = false;
                Myotismon.Enabled = false;
                MyotismonMaeson2.Enabled = false;
                Myotismon.Checked = false;
                MyotismonMaeson2.Checked = false;

            }
            else
            {
                restoreFilth.Checked = false;
                restoreFilth.Enabled = false;
                UnlockAreasVice.Enabled = true;
                Myotismon.Enabled = true;
                MyotismonMaeson2.Enabled = true;
            }
        }

        private void restoreFilth_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetRestoreFilth(restoreFilth.Checked);
        }
        

        private void Monochromon_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetHardMono(Monochromon.Checked);
        }

        private void tournaments_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetHardTourney(tournaments.Checked);
        }

        private void InfoDifficulty_Click(object sender, EventArgs e)
        {
            string tempText = "These are patches that will make the game more difficult, recommended for veteran players who want a challenge." + Environment.NewLine + Environment.NewLine +
               "EXTRA DIFFICULTY PATCHES" + Environment.NewLine + Environment.NewLine +
               "- Fair battles: you will be unable to run from battle and unable to use items in battle" + Environment.NewLine + Environment.NewLine +
               "- Hardcore Monochromon: The bits requirement to recruit Monochromon is increased to 4096" + Environment.NewLine + Environment.NewLine +
               "- Hardcore tournaments: Tournaments will have the stats of the digimon buffed" + Environment.NewLine + Environment.NewLine +
               "- Progression mode: click on the question mark next to it for detailed info" + Environment.NewLine + Environment.NewLine +
               "- True hardcore: makes hardcore even more difficult but without imposing restrictions, can be mixed with other patches." + Environment.NewLine + Environment.NewLine;

            if (parentForm.GetPatchType() == MainApp.patchType.NONE)
            {
                tempText = tempText + "- Harder bosses: click on the question mark next to it for detailed info" + Environment.NewLine + Environment.NewLine + Environment.NewLine;
            }
            else
                tempText = tempText + Environment.NewLine;
           

               tempText = tempText + "FILTH CHALLENGE" + Environment.NewLine + Environment.NewLine +
               "- Vice filth challenge: click on the question mark next to it for detailed info" + Environment.NewLine + Environment.NewLine +
               "- Original training/virus: It will enable the training debuff for the filth digimon and set the increase of the virus bar to the original value";

            ViceHackInfo infoDifficulty = new ViceHackInfo();
            infoDifficulty.ChangeTitle("Difficulty patches info");
            infoDifficulty.ChangeText(tempText);
            infoDifficulty.DisableLink();
            infoDifficulty.ShowDialog();

        }

        private void FlithInfo_Click(object sender, EventArgs e)
        {
            string tempText = "Filth challenge standard version. Not compatible with the 'Vice Unlock areas' patch." + Environment.NewLine + Environment.NewLine +
               "You will be limited to only filth digimon and filth techs, making it a full filth only run." + Environment.NewLine + Environment.NewLine +
               "Features:" + Environment.NewLine + Environment.NewLine +
               "- The game will be limited to fresh, in-training, only 2 rookies and only 4 ultimate (originally champions)" + Environment.NewLine + Environment.NewLine +
               "- The debuff from some filth digimon during training has been removed" + Environment.NewLine + Environment.NewLine +
               "- The virus bar increase will be faster (only 4 poops are required to turn into a Sukamon now)" + Environment.NewLine + Environment.NewLine +
               "- All areas are fully unlocked" + Environment.NewLine + Environment.NewLine +
               "- It is possible to participate in all the tournaments with this patch.";

            ViceHackInfo hardcoreHackInfo = new ViceHackInfo();
            hardcoreHackInfo.Text = "Info window";
            hardcoreHackInfo.ChangeTitle("Filth challenge info");
            hardcoreHackInfo.ChangeText(tempText);
            hardcoreHackInfo.ChangeLink("Spreadsheet with detailed information", "https://docs.google.com/spreadsheets/d/1I0rPMIKg5Q7H1EwDlyiOBTGRkKlFxaXM_O4I5mxMdfg/edit?usp=sharing");
            hardcoreHackInfo.ShowDialog();
        }

        private void InfoDigimon_Click(object sender, EventArgs e)
        {
            string tempText = "Patches that change digimon:" + Environment.NewLine + Environment.NewLine +
               "- Myotismon Maeson (Maeson hack only): click on the question mark next to it for detailed info" + Environment.NewLine + Environment.NewLine +
               "- Vermillimon patch: this patch changes the Monochromon texture to look like a Vermillimon, the chart also has a different texture" +Environment.NewLine + Environment.NewLine +
               "- Curling randomizer: click on the question mark next to it for detailed info";

            OptionalPatchesInfo infoWindow = new OptionalPatchesInfo();
            infoWindow.Text = "Info window";
            infoWindow.ChangeTitle("Digimon patches info");
            infoWindow.ChangeText(tempText);
            infoWindow.ShowDialog();
        }

        private void BugFixesInfo_Click(object sender, EventArgs e)
        {
            string tempText = "DO NOT APPLY THESE TO EITHER VICE OR MAESON, CHECK ALSO IF IT IS COMPATIBLE WITH THE RANDOMIZER HERE" + Environment.NewLine + Environment.NewLine + 
               "Patches that fix bugs/glitches from the game. These patches are only made for vanilla or a vanilla randomizer game, some patches may not be compatible with the randomizer:" + Environment.NewLine + Environment.NewLine +
               "- Bank text fix: fixes the bank text dissapearing when scrolling" + Environment.NewLine + Environment.NewLine +
               "- Battle text fix: this patch fixes an issue with a Clear Agumon in 'Toy Town' and a Blue Meramon in the 'Back Dimension' only spamming their text and not triggering a battle" + Environment.NewLine + Environment.NewLine +
               "- Battle time fix: this patch fixes various issues that happen when an hour passes while you are in a battle" + Environment.NewLine + Environment.NewLine +
               "- Evolution target fix (not compatible with the randomizer): fixes an oversight in the code that makes some digimon harder to obtain" + Environment.NewLine + Environment.NewLine +
               "- Evolution item flag fix: fixes an issue where the 'evolution item flag' is never deactivated after using an evolution item" + Environment.NewLine + Environment.NewLine +
               "- Evolution item rejection: fixes an issue where fresh/in-training can eat evo items, but not trigger the effect, when that's not intended" + Environment.NewLine + Environment.NewLine +
               "- Forget techs fix: this patch will fix the bug that deletes more techs than the intended ones when you forget techniques after losing all your lives" + Environment.NewLine + Environment.NewLine +
               "- Missing text fix: this patch restores missing text from various dialogues" + Environment.NewLine + Environment.NewLine +                
               "- Mojyamon fix: this patch fixes the Mojyamon in Freezeland, you will no longer get stuck inside them" + Environment.NewLine + Environment.NewLine +
               "- OmniDisk fix: the omnidisk works as intended now" + Environment.NewLine + Environment.NewLine +
               "- Prosperity medal fix: Fixes an issue where you cannot recieve the 'Prosperity medal' if you have the maximum tamer level" + Environment.NewLine + Environment.NewLine + 
               "- Rotation fix (not compatible with the randomizer): fixes various issues than can softlock the game related to rotations" + Environment.NewLine + Environment.NewLine +
               "- Recycling text fix: fixes a visual issue where the text in the recycling shop looks out of place" + Environment.NewLine + Environment.NewLine +
               "- Savedata text fix: fixes the digimon name issue from your save data" + Environment.NewLine + Environment.NewLine +
               "- Sleep hunger fix: Fixes a glitch where your digimon will never be hungry for an entire day (the glitch caused the digimon to lose energy and a lot of weight)" + Environment.NewLine + Environment.NewLine + 
               "- Softlock fixes (not compatible with the randomizer): fixes various possible softlocks" + Environment.NewLine + Environment.NewLine +                           
               "- Tankmon fix: this patch adds the missing tech from Tankmon" + Environment.NewLine + Environment.NewLine +
               "- Textbox choice fix: Fixes an issue where unintended script can run in three dialogue selections" + Environment.NewLine + Environment.NewLine + 
               "- Digitamamon restaurant fix : Fixes the exploit that allows you to eat as much as you want in Digitamamon's restaurant" + Environment.NewLine + Environment.NewLine +
               "- Giromon jukebox fix (not compatible with the randomizer fix version): Fixes the famous Giromon jukebox freeze" + Environment.NewLine + Environment.NewLine +
               "- MP Consumption fix: Fixes a bug where the game freezes if you get the MP reduction bonus in battle" + Environment.NewLine + Environment.NewLine +
               "- Sleep regeneration: Fixes an issue that can give you negative HP/MP if you sleep at a certain time with certain stats and items" + Environment.NewLine + Environment.NewLine +
               "- Sukamon evolution stats gain fix: Fixes an issue where naturally evolving from Sukamon will lower you stats" + Environment.NewLine + Environment.NewLine +               
               "- Tournament schedule fix: Fixes the tournament schedule being out of place";    

            ViceHackInfo infoWindow = new ViceHackInfo();
            infoWindow.Text = "Info window";
            infoWindow.ChangeTitle("Bug fixes info");
            infoWindow.ChangeText(tempText);
            infoWindow.ChangeLink("Check the wiki for more info", "https://github.com/Vicen04/Dw1DataAndPatches/wiki/Bugs,-glitches,-exploits-and-improvements");
            infoWindow.ShowDialog();
        }

        private void InfoUseful_Click(object sender, EventArgs e)
        {
            string tempText = "These are patches that will help the player:" + Environment.NewLine + Environment.NewLine +
               "- New stats gains: the stats gains formula after you win a battle has been changed: stat gain = (1% of digimon with the highest stat x number of enemies)" + Environment.NewLine + Environment.NewLine +
               "- Super stats gains: Uses the new formula and duplicates the result" + Environment.NewLine + Environment.NewLine +
               "- Ultra stats gains: Uses the new formula and multiplies the result by 10" + Environment.NewLine + Environment.NewLine +
               "- Stat gains evo item: Stat gains and extra lifespan are enabled when using an evolution item" + Environment.NewLine + Environment.NewLine +
               "- BGM patch: The background music will keep playing until you enter an area with different music rather than resetting in each map change." + Environment.NewLine + Environment.NewLine +
               "- Short intro: Makes the introduction a lot shorter" + Environment.NewLine + Environment.NewLine;

            if (parentForm.GetPatchType() == MainApp.patchType.NONE)
            {
                tempText = tempText +
                   "- Bonus try unrriged (not compatible with the randomizer version): The bonus try will no longer have the negative rigging" + Environment.NewLine + Environment.NewLine +
                   "- Super useful rigging: The help from the bonus try now will make you always land in the right stop" + Environment.NewLine + Environment.NewLine +
                   "- Ultra lucky bonus try: The chances of triggering the help are now 90% (60% normal help + 30% golden poop) + the effect from the super useful rigging patch" + Environment.NewLine + Environment.NewLine;
            }
            tempText = tempText +
               "- Drimogemon dirt reduction: The removal of dirt will be reduced to only 5 times" + Environment.NewLine + Environment.NewLine +
               "- Super dirt reduction: The removal of dirt will be reduced to only 2 times" + Environment.NewLine + Environment.NewLine +
               "- Super low Monochromon goal: You will only need 1280 bits to recruit Monochromon" + Environment.NewLine + Environment.NewLine +
               "- Helpful items: it does the following:" + Environment.NewLine + Environment.NewLine +
               " * The 'Steak' will now reduce the lifespan of a digimon to 0" + Environment.NewLine + Environment.NewLine +
               " * The vending machine at the 'Dragon Eye Lake' will sell 'Steaks' and 'Prickly Pears'" + Environment.NewLine + Environment.NewLine +
               "- Better drops: Digimon have better chances to drop an item, some digimon now drop different items" + Environment.NewLine + Environment.NewLine +
               "- Unlock Areas Vice: As long as you have the 'Mansion Key' in your inventory, any digimon will be able to enter the mansion. After recruiting Angemon, any digimon will be able to enter the Ice Sanctuary." + Environment.NewLine + Environment.NewLine +
               "- Better restaurant: the restaurant will give better stats, check the spreadsheet for details" + Environment.NewLine + Environment.NewLine +
               "- Better drimogemon: Drimogemon will give better rewards and lower the time it requires to find treasure, check the spreadsheet for details" + Environment.NewLine + Environment.NewLine +
               "- Better cards: Rare cards will be more frequent and the card merit value will be higher, check the spreadsheet for details." + Environment.NewLine + Environment.NewLine +
               "- Better merit: The merit shop will have more items and better prices, check the spreadsheet for details." + Environment.NewLine + Environment.NewLine +
               "- Better fishing: Fishing will become easier, check the spreadsheet for details." + Environment.NewLine + Environment.NewLine +
               "- Better item spawns: The items that randomly spawn will be more frequent, check the spreadsheet for details." + Environment.NewLine + Environment.NewLine +
               "- Better raise: Raising a digimon will be easier (evolution requirements are not affected), check the spreadsheet for details." + Environment.NewLine + Environment.NewLine +
               "- Better curling: The curling rewards will be better, check the spreadsheet for details." + Environment.NewLine + Environment.NewLine +
               "- Useful items 2: Kuwagamon and Kabuterimon will sell items at the Gym." + Environment.NewLine + Environment.NewLine +
               "- Training boost fix: This will make food not override the training boost, this means you can eat a meat after a supercarrot and you will still keep the boost to your training." + Environment.NewLine + Environment.NewLine +
               "- Easy medals: Some medals will become easier to obtain, these are:" + Environment.NewLine + Environment.NewLine +
               " * Fishing medal requires only 30 fish" + Environment.NewLine + Environment.NewLine +
               " * Tournament wins require only 30 wins" + Environment.NewLine + Environment.NewLine +
               " * Years medal requires to reach year 6" + Environment.NewLine + Environment.NewLine +
               " * Curling requires only 5 score" + Environment.NewLine + Environment.NewLine +
               "- Guaranteed rare spawns: Piximon, Mamemon, MetalMamemon and Otamamon will always appear." + Environment.NewLine + Environment.NewLine +
               "- Full time Seadramon: Seadramon appears at any time in the lake.";

            ViceHackInfo infoDifficulty = new ViceHackInfo();
            infoDifficulty.ChangeLink("Optional patches spreadsheet data", "https://docs.google.com/spreadsheets/d/1Wi1Cg0uHVHaEwUeSRae2neZoD93dYVkoHmjdqvDd9Ko/edit?usp=sharing");
            infoDifficulty.ChangeTitle("Useful patches info");
            infoDifficulty.ChangeText(tempText);
            infoDifficulty.ShowDialog();
        }

        private void InfoTechniques_Click(object sender, EventArgs e)
        {
            string tempText = "These are patches related to techniques:" + Environment.NewLine + Environment.NewLine +
               "- Better infinity burn: this patch will increase the activation speed of 'Infinity Burn' by one second as well as increase accuracy to a 90% and the stun chance to a 30%" + Environment.NewLine + Environment.NewLine +
               "- Better learn tech battle: this will improve the chances of learning a technique after a battle" + Environment.NewLine + Environment.NewLine +
               "- Better learn tech brains: this will improve the chances of learning a technique while training brains" + Environment.NewLine + Environment.NewLine +
               "- Learn multiple techs: this patch allows a player to learn more than one technique after a battle" + Environment.NewLine + Environment.NewLine +
               "- Nerf Ice Statue: this patch will lower Ice Statue power, accuracy and chance of trigger stun" + Environment.NewLine + Environment.NewLine +
               "- Insane Tech damage: this patch will change the range of the techniques damage from '90% - 110%' to '10% - 190%', finishers are not affected by this patch." + Environment.NewLine + Environment.NewLine +
               "- Skip orders text: this patch will skip the text when you learn new orders during brains training and in exchange, give you a chance to learn a technique.";

            HardcoreHackInfo infoWindow = new HardcoreHackInfo();
            infoWindow.Text = "Info window";
            infoWindow.ChangeTitle("Techniques patches info");
            infoWindow.ChangeText(tempText);
            infoWindow.ChangeLink("https://docs.google.com/spreadsheets/d/1OBg9Ke_JZ_8TjA62ldYacz5GYiKS0L-56FoG0JQe60Y/edit?usp=sharing", "Spreadsheet with the improved learn rate");
            infoWindow.ShowDialog();
        }

        private void Myotismon_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetMyotismon(Myotismon.Checked);
            if (Myotismon.Checked)
                MyotismonMaeson2.Checked = false;
        }

        private void Vermillimon_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetVermillimon(Vermillimon.Checked);
        }

        private void MyotismonInfo_Click(object sender, EventArgs e)
        {
            string tempText = "Only works with Maeson 1.0.9e or earlier versions. If you want to use the 'Tech Bonus Damage' patch from Maeson, apply that one before applying the Myotismon patch." + Environment.NewLine + Environment.NewLine +
               "This patch only works with the Maeson hack, it will exchange Machinedramon with Myotismon as a playable partner." + Environment.NewLine + Environment.NewLine +
               "Note that this has not been balanced by Maeson (just the raise data and stat gains, everything else is technically as how it should work) and it is just a representation of how Maeson's Myotismon would have worked. It is a fully playable patch and it has been tested. Now, about Myotismon:" + Environment.NewLine + Environment.NewLine +
               "- It has 4 techs: Giga Freeze, Megaton Punch, Thunder Justice and Hurricane (originally it had 5, but one of them had to be replaced to have a finisher)" + Environment.NewLine + Environment.NewLine +
               "- A finisher has been included: Glacial Blast. Panjyamon now has its original finisher (Fist of the Beast King)" + Environment.NewLine + Environment.NewLine +
               "- It has the exact same raise data as Machinedramon, so it will eat, sleep, poop and get tired in more or less the same way. The stats gains are also the same as Machinedramon.Maeson*probably* would have set this to be a bit different." + Environment.NewLine + Environment.NewLine +
               "- The original Myotismon only had the 'Thunder' and 'Battle' types, I had to add the 'Ice' type which was missing." + Environment.NewLine + Environment.NewLine + Environment.NewLine +
               "Other changes:" + Environment.NewLine + Environment.NewLine +
               "- Factorial town now has different evolutions: Tyranommon now evolves to Panjyamon with a 10 % chance, Angemon can evolve to Myotismon with a 10 % chance." + Environment.NewLine + Environment.NewLine + 
               "- The evo item 'MachiClaw' has been renamed 'DevilClaw', it is used to evolve to Myotismon." + Environment.NewLine + Environment.NewLine + Environment.NewLine +
               "Changes not visible:" + Environment.NewLine + Environment.NewLine +
               "- Both Machinedramon and Myotismon have mini sprites set for when you enter a tournament/cup, but since Maeson never allowed Machinedramon to enter any (and Myotismon follows the same logic), it is not visible at all." + Environment.NewLine + Environment.NewLine +
               "- Leomon and Panjyamon now share the same ID when it comes to check for techs enabled, originally Maeson decided to use a different one for Panjyamon." + Environment.NewLine + Environment.NewLine +
               "- Cleaned some leftover code that would have made this patch use the wrong audio for Myotismon, luckily using MetalGreymon sounds is not out of place for Machinedramon... but it is supposed to use Megadramon sounds as the game itself does that when you fight it. If it was not fixed, this Myotismon would also use the sounds of MetalGreymon.";

            ViceHackInfo infoDifficulty = new ViceHackInfo();
            infoDifficulty.DisableLink();
            infoDifficulty.ChangeTitle("Myotismon patch info");
            infoDifficulty.ChangeText(tempText);
            infoDifficulty.ShowDialog();
        }

        private void CurlingRandomizerInfo_Click(object sender, EventArgs e)
        {
            string tempText = "The curling randomizer makes you play with a random opponent when playing against Penguinmon. Rewards are still the same, the AI is also the same." + Environment.NewLine + Environment.NewLine +
            "Metal Mamemon matches are not afected by the randomizer, but playing against Penguinmon may make Metal Mamemon show up. " + Environment.NewLine + Environment.NewLine +
            "The randomizer includes all the digimon you have as a partner or you can battle against plus Jijimon, Market Manager, Shogun Gekkomon and King Sukamon." + Environment.NewLine + Environment.NewLine +
            "Some digimon will have a shorter name (such as 'platinumsukamo') due to the limitations the name displayed has, it has a limit of 14 characters." + Environment.NewLine + Environment.NewLine +
            "Some digimon may be displaced due to the animation, the position will be reset after the turn comes back to you.";

            OptionalPatchesInfo infoWindow = new OptionalPatchesInfo();
            infoWindow.Text = "Info window";
            infoWindow.ChangeTitle("Curling randomizer info");
            infoWindow.ChangeText(tempText);
            infoWindow.ShowDialog();
        }

        private void missingText_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetMissingText(missingText.Checked);
        }

        private void forgetTechs_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetForgetMoves(ForgetTechs.Checked);
        }

        private void battleText_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetBattleText(BattleText.Checked);
        }

        private void battleTime_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetBattleTime(BattleTime.Checked);
        }

        private void curlingRandomizer_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetCurlingRandomizer(curlingRandomizer.Checked);
        }

        private void multipleTechs_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetMultipleTechs(multipleTechs.Checked);
        }

        private void bettertechBattle_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetBetterBattleTech(bettertechBattle.Checked);
        }

        private void betterTechsBrains_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetBetterBrainTech(betterTechsBrains.Checked);
        }

        private void statsGains_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetStatsGains(statsGains.Checked);
            if (statsGains.Checked)
            {
                sStatsGains.Checked = false;
                uStatsGains.Checked = false;
            }
        }

        private void sStatsGains_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetSStatsGains(sStatsGains.Checked);
            if (sStatsGains.Checked)
            {
                statsGains.Checked = false;
                uStatsGains.Checked = false;
            }
        }

        private void uStatsGains_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetUStatsGains(uStatsGains.Checked);
            if (uStatsGains.Checked)
            {
                sStatsGains.Checked = false;
                statsGains.Checked = false;
            }
        }

        private void evoItem_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetEvoItem(evoItem.Checked);
        }

        private void ShortIntro_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetShortIntro(ShortIntro.Checked);
            
        }

        private void sBonusTry_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetSuperBonus(sBonusTry.Checked);
            if (sBonusTry.Checked)
                uBonustry.Checked = false;
        }

        private void uBonustry_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetUltraBonus(uBonustry.Checked);
            if (uBonustry.Checked)
                sBonusTry.Checked = false;
        }

        private void dirtReduction_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetDirtReduction(dirtReduction.Checked);
            if (dirtReduction.Checked)
                sDirtReduction.Checked = false;
        }

        private void sDirtReduction_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetSDirtReduction(sDirtReduction.Checked);
            if (sDirtReduction.Checked)
                dirtReduction.Checked = false;
        }

        private void lessMono_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetLessMono(lessMono.Checked);
        }

        private void helpfulItems_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetHelpfulItems(helpfulItems.Checked);
        }

        private void betterDrops_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetBetterDrops(betterDrops.Checked);
        }

        private void MpRedFix_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetMPText(MpRedFix.Checked);
        }

        private void tankmonFix_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetTankmon(tankmonFix.Checked);
        }

        private void Giromon_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetGiromon(Giromon.Checked);
        }

        public void EndPatchingApp(object sender, EventArgs e)
        {
            parentForm.patchFinished -= EndPatchingApp;
            tempConfirm.FormClosed += new FormClosedEventHandler(CloseApp);
            tempConfirm.ConfirmExit();
            
        }

        void CloseApp(object sender, EventArgs e) { this.Close(); }

        private void HarderBosses_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetHardmode(HarderBosses.Checked);
        }

        private void HarderBossesInfo_Click(object sender, EventArgs e)
        {
            string tempText = "A patch that makes some boss battles harder." + Environment.NewLine + Environment.NewLine +
                "Features:" + Environment.NewLine + Environment.NewLine +
                "- Better moveset for some boss battles" + Environment.NewLine + Environment.NewLine +
                "- Two buffed bosses.";

            HardcoreHackInfo hardcoreHackInfo = new HardcoreHackInfo();
            hardcoreHackInfo.Text = "Info window";
            hardcoreHackInfo.ChangeTitle("Harder bosses info");
            hardcoreHackInfo.ChangeText(tempText);
            hardcoreHackInfo.ChangeLink("https://docs.google.com/spreadsheets/d/14wAuaMaLK6YZqwGXQSaG4RKCBfH6JWnrXg2cVAL_KW4/edit?usp=sharing", "Spreadsheet with detailed information");
            hardcoreHackInfo.ShowDialog();
        }

        private void MojyamonFix_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetMojyamon(MojyamonFix.Checked);
        }

        private void rotationFix_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetRotation(rotationFix.Checked);
        }

        private void TournamentSchedule_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetTourney(TournamentSchedule.Checked);
        }

        private void OmnidiskFix_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetOmniDisk(OmnidiskFix.Checked);
        }

        private void SoftlockFix_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetSoftlocks(SoftlockFix.Checked);
        }

        private void digitamamon_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetDigitamamon(digitamamon.Checked);
        }

        private void savedataText_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetSaveData(savedataText.Checked);
        }

        private void bankText_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetBankText(bankText.Checked);
        }

        private void textboxChoice_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetTextboxChoice(textboxChoice.Checked);
        }

        private void sleepHunger_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetSleepHunger(sleepHunger.Checked);
        }

        private void sleepRegen_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetSleepRegen(sleepRegen.Checked);
        }

        private void EvoTarget_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetEvoTarget(EvoTarget.Checked);
        }

        private void EvoReject_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetItemReject(EvoReject.Checked);
        }

        private void evoItemFlag_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetEvoItemFlag(evoItemFlag.Checked);
        }

        private void Sukamonfix_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetSukamon(Sukamonfix.Checked);
        }

        private void prosperityFix_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetProsperity(prosperityFix.Checked);
        }

        private void BGMpatch_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetBGMPatch(BGMpatch.Checked);
        }

        private void betterInfinity_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetInfinityBurn(betterInfinity.Checked);
        }

        private void recycling_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetRecycling(recycling.Checked);
        }

        private void bonusTryFix_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetBonusTry(bonusTryFix.Checked);

            if (bonusTryFix.Checked)
            {
                sBonusTry.Enabled = true;
                uBonustry.Enabled = true;
            }
            else
            {
                sBonusTry.Enabled = false;
                sBonusTry.Checked = false;
                uBonustry.Checked = false;
                uBonustry.Enabled = false;
            }
        }

        private void UnlockAreasVice_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetUnlockAreas(UnlockAreasVice.Enabled);
        }

        private void MyotismonMaeson2_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetMyotismon2(MyotismonMaeson2.Checked);
            if (MyotismonMaeson2.Checked)
                Myotismon.Checked = false;
        }

        private void MyotismonInfo2_Click(object sender, EventArgs e)
        {
            string tempText = "Only works with Maeson 1.0.10 or later versions. If you want to use the 'Tech Bonus Damage' patch from Maeson, apply that one before applying the Myotismon patch." + Environment.NewLine + Environment.NewLine +
               "This patch only works with the Maeson hack, it will exchange Machinedramon with Myotismon as a playable partner." + Environment.NewLine + Environment.NewLine +
               "Note that this has not been balanced by Maeson (just the raise data and stat gains, everything else is technically as how it should work) and it is just a representation of how Maeson's Myotismon would have worked. It is a fully playable patch and it has been tested. Now, about Myotismon:" + Environment.NewLine + Environment.NewLine +
               "- It has 4 techs: Giga Freeze, Megaton Punch, Thunder Justice and Hurricane(originally it had 5, but one of them had to be replaced to have a finisher)" + Environment.NewLine + Environment.NewLine +
               "- A finisher has been included: Glacial Blast. Panjyamon now has its original finisher (Fist of the Beast King)" + Environment.NewLine + Environment.NewLine +
               "- It has the exact same raise data as Machinedramon, so it will eat, sleep, poop and get tired in more or less the same way. The stats gains are also the same as Machinedramon.Maeson*probably* would have set this to be a bit different." + Environment.NewLine + Environment.NewLine +
               "- The original Myotismon only had the 'Thunder' and 'Battle' types, I had to add the 'Ice' type which was missing." + Environment.NewLine + Environment.NewLine + Environment.NewLine +
               "Other changes:" + Environment.NewLine + Environment.NewLine +
               "- Factorial town now has different evolutions: Tyranommon now evolves to Panjyamon with a 10 % chance, Angemon can evolve to Myotismon with a 10 % chance." + Environment.NewLine + Environment.NewLine +
               "- The evo item 'Analogizer' has been renamed 'Researcher', it is used to evolve to Myotismon." + Environment.NewLine + Environment.NewLine + Environment.NewLine +
               "Changes not visible:" + Environment.NewLine + Environment.NewLine +
               "- Both Machinedramon and Myotismon have mini sprites set for when you enter a tournament/cup, but since Maeson never allowed Machinedramon to enter any (and Myotismon follows the same logic), it is not visible at all." + Environment.NewLine + Environment.NewLine +
               "- Leomon and Panjyamon now share the same ID when it comes to check for techs enabled, originally Maeson decided to use a different one for Panjyamon." + Environment.NewLine + Environment.NewLine +
               "- Cleaned some leftover code that would have made this patch use the wrong audio for Myotismon, luckily using MetalGreymon sounds is not out of place for Machinedramon... but it is supposed to use Megadramon sounds as the game itself does that when you fight it. If it was not fixed, this Myotismon would also use the sounds of MetalGreymon.";

            ViceHackInfo infoDifficulty = new ViceHackInfo();
            infoDifficulty.DisableLink();
            infoDifficulty.ChangeTitle("Myotismon patch info");
            infoDifficulty.ChangeText(tempText);
            infoDifficulty.ShowDialog();
        }

        private void ProgressionInfo_Click(object sender, EventArgs e)
        {
            string tempText = "A patch that only lets you learn techniques from recruits." + Environment.NewLine + Environment.NewLine +
                              "Features:" + Environment.NewLine + Environment.NewLine +
                              "- You can only learn the weakest techniques and 2 filth techs naturally" + Environment.NewLine + Environment.NewLine +
                              "- Recruitments and some boss battles will teach you techniques.";

            HardcoreHackInfo hardcoreHackInfo = new HardcoreHackInfo();
            hardcoreHackInfo.Text = "Info window";
            hardcoreHackInfo.ChangeTitle("Progression info");
            hardcoreHackInfo.ChangeText(tempText);
            hardcoreHackInfo.ChangeLink("https://docs.google.com/spreadsheets/d/147dr8Uq_LT1X0STYwuvS59PwbZ_l4hK97GsVCvzJISU/edit?usp=sharing", "Spreadsheet with detailed information");
            hardcoreHackInfo.ShowDialog();
        }

        private void Progression_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetProgression(Progression.Checked);
            if (Progression.Checked)
            {
                bettertechBattle.Enabled = false;
                betterTechsBrains.Enabled = false;
                betterTechsBrains.Checked = false;
                bettertechBattle.Checked = false;
            }
            else
            {
                bettertechBattle.Enabled = true;
                betterTechsBrains.Enabled = true;
            }
        }

        private void LeftUseful_Click(object sender, EventArgs e)
        {
            RightUseful.Enabled = true;
            RightUseful.Visible = true;
            statsGains.Enabled = true;
            statsGains.Visible = true;
            sStatsGains.Visible = true;
            uStatsGains.Visible = true;
            statsGains.Enabled = true;
            uStatsGains.Enabled = true;
            evoItem.Enabled = true;
            evoItem.Visible = true;
            BGMpatch.Visible = true;
            BGMpatch.Enabled = true;
            ShortIntro.Visible = true;
            ShortIntro.Enabled = true;
            dirtReduction.Visible = true;
            dirtReduction.Enabled = true;
            sDirtReduction.Visible = true;
            sDirtReduction.Enabled = true;
            lessMono.Visible = true;
            lessMono.Enabled = true;
            helpfulItems.Visible = true;
            helpfulItems.Enabled = true;
            medals.Visible = true;
            medals.Enabled = true;
            rareSpawn.Visible = true;
            rareSpawn.Enabled = true;


            if (parentForm.GetPatchType() == MainApp.patchType.NONE)
            {
                bonusTryFix.Enabled = true;
                bonusTryFix.Visible = true;
                uBonustry.Visible = true;            
                sBonusTry.Visible = true;
                if (bonusTryFix.Checked)
                {
                    uBonustry.Enabled = true;
                    sBonusTry.Enabled = true;
                }
            }

            LeftUseful.Enabled = false;
            LeftUseful.Visible = false;
            betterDrops.Visible = false;
            betterDrops.Enabled = false;
            UnlockAreasVice.Visible = false;
            UnlockAreasVice.Enabled = false;
            betterDrimogemon.Visible = false;
            betterDrimogemon.Enabled = false;
            UsefulItems2.Enabled = false;
            UsefulItems2.Visible = false;
            BetterRestaurant.Visible = false;
            BetterRestaurant.Enabled = false;
            BetterCurling.Visible = false;
            BetterCurling.Enabled = false;
            BetterMerit.Visible = false;
            BetterMerit.Enabled = false;
            BetterCards.Enabled = false;
            BetterCards.Visible = false;
            BetterFishing.Visible = false;
            BetterFishing.Enabled = false;
            BetterRaise.Enabled = false;
            BetterRaise.Visible = false;
            BetterItemSpawns.Visible = false;
            BetterItemSpawns.Enabled = false;
            trainingBoost.Enabled = false;
            trainingBoost.Visible = false;
            Seadramon.Visible = false;
            Seadramon.Enabled = false;
        }

        private void RightUseful_Click(object sender, EventArgs e)
        {
            LeftUseful.Enabled = true;
            LeftUseful.Visible = true;
            betterDrops.Visible = true;
            betterDrops.Enabled = true;
            UnlockAreasVice.Visible = true;
            UnlockAreasVice.Enabled = true;
            betterDrimogemon.Visible = true;
            betterDrimogemon.Enabled = true;
            UsefulItems2.Enabled = true;
            UsefulItems2.Visible = true;
            BetterRestaurant.Visible = true;
            BetterRestaurant.Enabled = true;
            BetterCurling.Visible = true;
            BetterCurling.Enabled = true;
            BetterMerit.Visible = true;
            BetterMerit.Enabled = true;
            BetterCards.Enabled = true;
            BetterCards.Visible = true;
            BetterFishing.Visible = true;
            BetterFishing.Enabled = true;
            BetterRaise.Enabled = true;
            BetterRaise.Visible = true;
            BetterItemSpawns.Visible = true;
            BetterItemSpawns.Enabled = true;
            trainingBoost.Enabled = true;
            trainingBoost.Visible = true;
            Seadramon.Visible = true;
            Seadramon.Enabled = true;


            RightUseful.Enabled= false;
            RightUseful.Visible = false;
            statsGains.Enabled = false;
            statsGains.Visible = false;
            sStatsGains.Visible = false;
            uStatsGains.Visible = false;
            statsGains.Enabled = false;
            uStatsGains.Enabled = false;
            evoItem.Enabled = false;
            evoItem.Visible = false;
            BGMpatch.Visible = false;
            BGMpatch.Enabled = false;
            ShortIntro.Visible = false;
            ShortIntro.Enabled = false;
            bonusTryFix.Enabled = false;
            bonusTryFix.Visible = false;
            uBonustry.Visible = false;
            uBonustry.Enabled = false;
            sBonusTry.Visible = false;
            sBonusTry.Enabled = false;
            dirtReduction.Visible = false;
            dirtReduction.Enabled = false;
            sDirtReduction.Visible = false;
            sDirtReduction.Enabled = false;
            lessMono.Visible = false;
            lessMono.Enabled = false;
            helpfulItems.Visible = false;
            helpfulItems.Enabled = false;
            medals.Visible = false;
            medals.Enabled = false;
            rareSpawn.Visible = false;
            rareSpawn.Enabled = false;

        }

        private void BetterRestaurant_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetRestaurant(BetterRestaurant.Checked);
        }

        private void BetterCards_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetCards(BetterCards.Checked);
        }

        private void BetterFishing_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetFishing(BetterFishing.Checked);
        }

        private void betterDrimogemon_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetDrimogemon(betterDrimogemon.Checked);
        }

        private void BetterCurling_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetCurling(BetterCurling.Checked);
        }

        private void BetterRaise_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetRaise(BetterRaise.Checked);
        }

        private void BetterMerit_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetMerit(BetterMerit.Checked);
        }

        private void UsefulItems2_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetUseful2(UsefulItems2.Checked);
        }

        private void BetterItemSpawns_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetItemSpawns(BetterItemSpawns.Checked);
        }

        private void NerfIce_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetIceNerf(NerfIce.Checked);
        }

        private void TrainingBoost_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetTrainingBoost(trainingBoost.Checked);
        }

        private void insaneDamage_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetInsaneDamage(insaneDamage.Checked);
        }

        private void trueHardcore_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetTrueHardcore(trueHardcore.Checked);
        }

        private void noOrders_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetOrders(noOrders.Checked);
        }

        private void Seadramon_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetSeadramon(Seadramon.Checked);
        }

        private void rareSpawn_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetRareSpawns(rareSpawn.Checked);
        }

        private void medals_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetEasyMedals(medals.Checked);
        }
    }
}
