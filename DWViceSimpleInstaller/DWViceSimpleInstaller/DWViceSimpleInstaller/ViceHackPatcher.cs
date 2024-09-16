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
    public partial class ViceHackPatcher : Form
    {
        private OpenFileDialog openFileDialog;
        private string filePath = string.Empty;
        MainApp parentForm;
        Button[] patchTypes;
        Button[] patchTypesInfo;
        GroupBox[] patchesContainers;
        PatchConfirm tempConfirm;
        public ViceHackPatcher(MainApp oldForm)
        {
            InitializeComponent();            
            parentForm = oldForm;
            parentForm.patchFinished += new EventHandler(EndPatchingApp);
            patchTypes = new Button[5] { GameplayPatches, DigimonPatches, MiscellaneousPatches, UsefulPatches, TechPatches };
            patchTypesInfo = new Button[5] {InfoDifficulty, InfoDigimon, InfoMiscellaneous, InfoUseful, InfoTechniques};
            patchesContainers = new GroupBox[5] {usefulContainer, techContainer, miscPatches, DifficultyPatchesContainer, DigimonGroup };
            this.FormClosed += new FormClosedEventHandler(parentForm.RestartBools);
        }
        

        private void ViceHackPatcher_Load(object sender, EventArgs e)
        {
            openFileDialog = new OpenFileDialog()
            {
                Filter = "BIN files (*.BIN)|*.BIN| ISO files (*.ISO)|*.ISO",
                FileName = "Select the game file",
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

        private void MiscellaneousPatches_Click(object sender, EventArgs e)
        {
            ChangeButtonColours(MiscellaneousPatches);
            EnableContainer(miscPatches);
            EnableInfo(InfoMiscellaneous);
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

        private void HardcoreInfo_Click(object sender, EventArgs e)
        {
            string tempText = "Version 1.0.3" + Environment.NewLine + Environment.NewLine +
                "Vice hack version of the hardcore hack. Applying this patch will disable the 'super useful' and 'ultra lucky' useful patches" + Environment.NewLine + Environment.NewLine +
                "It will increase the difficulty of the game by modifying the stats and moveset of the NPC in the game and add some new content." + Environment.NewLine + Environment.NewLine +
                "Some of the features are:" + Environment.NewLine + Environment.NewLine +
                "- More stats for all the NPC digimon" + Environment.NewLine + Environment.NewLine +
                "- Different movesets for the NPC digimon (such as removing slow weak techs like 'static elec')." + Environment.NewLine + Environment.NewLine +
                "- New boss battles" + Environment.NewLine + Environment.NewLine +
                "- NPCs will now have the tech boost feature" + Environment.NewLine + Environment.NewLine +
                "- 1.0.3 addition: The bonus try is now rigged again and it will force you to fail, if you want to unrig it, you can use the optional installer.";


            HardcoreHackInfo hardcoreHackInfo = new HardcoreHackInfo();
            hardcoreHackInfo.Text = "Info window";
            hardcoreHackInfo.ChangeTitle("Vice hardcore info");
            hardcoreHackInfo.ChangeText(tempText);
            hardcoreHackInfo.ShowDialog();
        }

        private void PatchButton_Click(object sender, EventArgs e)
        {
            parentForm.SetFilePath(filePath);
            tempConfirm = new PatchConfirm(parentForm);
            tempConfirm.SetFileName("Digimon World Vice");
            tempConfirm.ShowDialog();
        }

        private void ChallengePatch_CheckedChanged(object sender, EventArgs e)
        {
            if (ChallengePatch.Checked)
            {
                Hardcore.Checked = false;
                Hardmode.Checked = false;
                hardcoreBattler.Checked = false;
                hardcoreBattler.Enabled = false;
                ultraHardcore.Checked = false;
                ultraHardcore.Enabled = false;
                removeTechBoost.Enabled = false;
                removeTechBoost.Checked = false;
                parentForm.SetViceDifficulty(MainApp.viceDifficulty.CHALLENGE);
            }
            else
            {
                removeTechBoost.Enabled = true;
                hardcoreBattler.Enabled = true;
                if (!Hardcore.Checked && !Hardmode.Checked)
                    parentForm.SetViceDifficulty(MainApp.viceDifficulty.NONE);
            }
        }

        private void Hardmode_CheckedChanged(object sender, EventArgs e)
        {
            if (Hardmode.Checked)
            {
                ChallengePatch.Checked = false;
                Hardcore.Checked = false;
                ultraHardcore.Enabled = true;
                parentForm.SetViceDifficulty(MainApp.viceDifficulty.HARDMODE);
            }
            else
            {
                if (!Hardcore.Checked && !ChallengePatch.Checked)
                    parentForm.SetViceDifficulty(MainApp.viceDifficulty.NONE);

                if (!Hardcore.Checked)
                {
                    ultraHardcore.Checked = false;
                    ultraHardcore.Enabled = false;
                }
            }
        }

        private void Hardcore_CheckedChanged(object sender, EventArgs e)
        {
            if (Hardcore.Checked)
            {
                ChallengePatch.Checked = false;
                Hardmode.Checked = false;                
                ultraHardcore.Enabled = true;
                parentForm.SetViceDifficulty(MainApp.viceDifficulty.HARDCORE);
                sBonusTry.Checked = false;
                uBonustry.Checked = false;
                sBonusTry.Enabled = false;
                uBonustry.Enabled = false;
            }
            else
            {
                if (!Hardmode.Checked && !ChallengePatch.Checked)                
                    parentForm.SetViceDifficulty(MainApp.viceDifficulty.NONE);                

                if (!Hardmode.Checked)
                {
                    ultraHardcore.Checked = false;
                    ultraHardcore.Enabled = false;
                }

                sBonusTry.Enabled = true;
                uBonustry.Enabled = true;
            }
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
                Myotismon.Enabled = false;
                Myotismon.Checked = false;
            }
            else
            {
                restoreFilth.Checked = false;
                restoreFilth.Enabled = false;
                Myotismon.Enabled = true;
            }
        }

        private void restoreFilth_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetRestoreFilth(restoreFilth.Checked);
        }

        private void ultraHardcore_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetUltraHardcore(ultraHardcore.Checked);
            if (ultraHardcore.Checked)
            {
                hardcoreBattler.Enabled = false;
                hardcoreBattler.Checked = false;                
                removeTechBoost.Enabled = false;
                removeTechBoost.Checked = false;
            }
            else
            {
                removeTechBoost.Enabled = true;

                if (Hardcore.Checked || Hardmode.Checked)                
                    hardcoreBattler.Enabled = true;                
                else if (!ChallengePatch.Checked)
                    hardcoreBattler.Enabled = true;
            }
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
               "MAIN PATCHES" + Environment.NewLine + Environment.NewLine +
               "Only one patch of this type can be applied due to conflicting data, choose the best suited for you:" + Environment.NewLine + Environment.NewLine +
               "- Challenge patch: click on the question mark next to it for detailed info" + Environment.NewLine + Environment.NewLine +
               "- Vice hardmode: click on the question mark next to it for detailed info" + Environment.NewLine + Environment.NewLine +
               "- Vice hardcore: click on the question mark next to it for detailed info" + Environment.NewLine + Environment.NewLine + Environment.NewLine +
               "EXTRA DIFFICULTY PATCHES" + Environment.NewLine + Environment.NewLine +
               "Can be mixed with other patches or applied as a standalone:" + Environment.NewLine + Environment.NewLine +
               "- Fair battles: you will be unable to run from battle and unable to use items in battle" + Environment.NewLine + Environment.NewLine +
               "- Hardcore Monochromon: The bits requirement to recruit Monochromon is increased to 4096" + Environment.NewLine + Environment.NewLine +
               "- Hardcore tournaments: Tournaments will have the stats of the digimon buffed" + Environment.NewLine + Environment.NewLine +
               "- Ultra hardcore mode: you will be unable to run from battle and unable to use items in battle, on top of that, the tech boost for your digimon will be disabled and all of the NPCs will have a tech boost applied to all the normal techs" + Environment.NewLine + Environment.NewLine + Environment.NewLine +
               "FILTH CHALLENGE" + Environment.NewLine + Environment.NewLine +
               "Can be mixed with other patches or applied as a standalone:" + Environment.NewLine + Environment.NewLine +
               "- Vice filth challenge: click on the question mark next to it for detailed info" + Environment.NewLine + Environment.NewLine +
               "- Original training/virus: It will enable the training debuff for the filth digimon and set the increase of the virus bar to the original value";

            ViceHackInfo infoDifficulty = new ViceHackInfo();
            infoDifficulty.DisableLink();
            infoDifficulty.ChangeTitle("Difficulty patches info");
            infoDifficulty.ChangeText(tempText);
            infoDifficulty.ShowDialog();
        }

        private void ChallengeInfo_Click(object sender, EventArgs e)
        {
            string tempText = "This is a challenge run for the experienced players." + Environment.NewLine + Environment.NewLine +
               "This patch will do the following:" + Environment.NewLine + Environment.NewLine +
               "- You will be unable to run from battle" + Environment.NewLine + Environment.NewLine +
               "- You will be unable to use items in battle" + Environment.NewLine + Environment.NewLine +
               "- The tech boost has been disabled for your digimon, the tech boost is now applied to all NPCs";

            OptionalPatchesInfo infoWindow = new OptionalPatchesInfo();
            infoWindow.Text = "Info window";
            infoWindow.ChangeTitle("Challenge patch info");
            infoWindow.ChangeText(tempText);
            infoWindow.ShowDialog();

        }

        private void hardmodeInfo_Click(object sender, EventArgs e)
        {
            string tempText = "Vice hack version of the harder bosses patch" + Environment.NewLine + Environment.NewLine +
                "It will increase the difficulty of some boss battles." + Environment.NewLine + Environment.NewLine +
                "Features:" + Environment.NewLine + Environment.NewLine +
                "- Better moveset for some boss battles" + Environment.NewLine + Environment.NewLine +
                "- Two buffed bosses." + Environment.NewLine + Environment.NewLine +
                "- NPCs will now have the tech boost feature";

            HardcoreHackInfo hardcoreHackInfo = new HardcoreHackInfo();
            hardcoreHackInfo.Text = "Info window";
            hardcoreHackInfo.ChangeTitle("Vice hardmode info");
            hardcoreHackInfo.ChangeText(tempText);
            hardcoreHackInfo.ChangeLink("https://docs.google.com/spreadsheets/d/14wAuaMaLK6YZqwGXQSaG4RKCBfH6JWnrXg2cVAL_KW4/edit?usp=sharing", "Spreadsheet with detailed information");
            hardcoreHackInfo.ShowDialog();
      
        }

        private void FlithInfo_Click(object sender, EventArgs e)
        {
            string tempText = "Vice hack version of the Filth challenge." + Environment.NewLine + Environment.NewLine +
               "You will be limited to only filth digimon and filth techs, making it a full filth only run." + Environment.NewLine + Environment.NewLine +
               "Features:" + Environment.NewLine + Environment.NewLine +
               "- The game will be limited to fresh, in-training, only 2 rookies and only 4 ultimate (originally champions)" + Environment.NewLine + Environment.NewLine +
               "- The debuff from some filth digimon during training has been removed" + Environment.NewLine + Environment.NewLine +
               "- The virus bar increase will be faster (only 4 poops are required to turn into a Sukamon now)" + Environment.NewLine + Environment.NewLine +
               "- All areas are fully unlocked" + Environment.NewLine + Environment.NewLine +
               "- It is possible to participate in all the tournaments with this patch.";

            HardcoreHackInfo hardcoreHackInfo = new HardcoreHackInfo();
            hardcoreHackInfo.Text = "Info window";
            hardcoreHackInfo.ChangeTitle("Vice filth challenge info");
            hardcoreHackInfo.ChangeText(tempText);
            hardcoreHackInfo.ChangeLink("https://docs.google.com/spreadsheets/d/1I0rPMIKg5Q7H1EwDlyiOBTGRkKlFxaXM_O4I5mxMdfg/edit?usp=sharing", "Spreadsheet with detailed information");
            hardcoreHackInfo.ShowDialog();
        }

        private void InfoDigimon_Click(object sender, EventArgs e)
        {
            string tempText = "Patches that change digimon:" + Environment.NewLine + Environment.NewLine +
               "- Myotismon patch: click on the question mark next to it for detailed info" + Environment.NewLine + Environment.NewLine +
               "- Vermillimon patch: this patch changes the Monochromon texture to look like a Vermillimon, the chart also has a different texture" +Environment.NewLine + Environment.NewLine +
               "- Curling randomizer: click on the question mark next to it for detailed info";

            OptionalPatchesInfo infoWindow = new OptionalPatchesInfo();
            infoWindow.Text = "Info window";
            infoWindow.ChangeTitle("Digimon patches info");
            infoWindow.ChangeText(tempText);
            infoWindow.ShowDialog();
        }

        private void InfoMiscellaneous_Click(object sender, EventArgs e)
        {
            string tempText = "Patches that do not fall in any specific category:" + Environment.NewLine + Environment.NewLine +
               "RANDOMIZER COMPATIBILITY" + Environment.NewLine + Environment.NewLine +
               "Patches that can be used to make the hack compatible with the randomizer, these patches only work if you applied the randomizer before this hack" + Environment.NewLine + Environment.NewLine +
               "- Randomizer adaptor: this patch makes a randomized game file become compatible with the Vice hack" + Environment.NewLine + Environment.NewLine +
               "- Factorial Town fix: this patch will make the new evolutions in Factorial Town work" + Environment.NewLine + Environment.NewLine + Environment.NewLine +
               "OTHER PATCHES" + Environment.NewLine + Environment.NewLine +
               "- Insane battle: this patch makes the Metal Etemon battle work in a weird way" + Environment.NewLine + Environment.NewLine +
               "- Fully unlock areas: this patch will let you enter the Mansion, Sanctuary and Toy Town without any restrictions" + Environment.NewLine + Environment.NewLine +
               "- Restore chain melon: the HappyMushroom and the Chain Melon will have their original use" + Environment.NewLine + Environment.NewLine +
               "- Remove tech boost: this patch will remove the tech boost of all digimon (not available in challenge or ultra hardcore)" + Environment.NewLine + Environment.NewLine +
               "- Restore ultimate extended lifetime: The extended lifetime of 8 days when becoming an ultimate, will go back to the original 4 days" + Environment.NewLine + Environment.NewLine +
               "- Map colour fix: Fixes the coloring issue in the bonus map, showing the original intended texture and making it better for the eyes.";


            ViceHackInfo infoWindow = new ViceHackInfo();
            infoWindow.Text = "Info window";
            infoWindow.ChangeTitle("Miscellaneous patches info");
            infoWindow.ChangeText(tempText);
            infoWindow.DisableLink();
            infoWindow.ShowDialog();
        }

        private void InfoUseful_Click(object sender, EventArgs e)
        {
            string tempText = "These are patches that will help the player:" + Environment.NewLine + Environment.NewLine +              
               "- New stats gains: the stats gains formula after you win a battle has been changed: stat gain = (1% of digimon with the highest stat x number of enemies)" + Environment.NewLine + Environment.NewLine +
               "- Super stats gains: Uses the new formula and duplicates the result" + Environment.NewLine + Environment.NewLine +
               "- Ultra stats gains: Uses the new formula and multiplies the result by 10" + Environment.NewLine + Environment.NewLine + 
               "- Stat gains evo item: Stat gains and extra lifespan are enabled when using an evolution item" + Environment.NewLine + Environment.NewLine +
               "- Short intro: Makes the introduction a lot shorter" + Environment.NewLine + Environment.NewLine +               
               "- Super useful rigging: The help from the bonus try now will make you always land in the right stop" + Environment.NewLine + Environment.NewLine +
               "- Ultra lucky bonus try: The chances of triggering the help are now 90% (60% normal help + 30% golden poop) + the effect from the super useful rigging patch" + Environment.NewLine + Environment.NewLine +
               "- Drimogemon dirt reduction: The removal of dirt will be reduced to only 5 times" + Environment.NewLine + Environment.NewLine +
               "- Super dirt reduction: The removal of dirt will be reduced to only 2 times" + Environment.NewLine + Environment.NewLine +
               "- Super low Monochromon goal: You will only need 1280 bits to recruit Monochromon" + Environment.NewLine + Environment.NewLine +
               "- Helpful items: it does the following:" + Environment.NewLine + Environment.NewLine +
               " * The 'Steak' will now reduce the lifespan of a digimon to 0" + Environment.NewLine + Environment.NewLine +
               " * The vending machine at the 'Dragon Eye Lake' will sell 'Steaks' and 'Prickly Pears'" + Environment.NewLine + Environment.NewLine +
               "- Better drops: Digimon have better chances to drop an item, some digimon now drop different items";

            ViceHackInfo infoDifficulty = new ViceHackInfo();
            infoDifficulty.ChangeLink("Better item drops spreadsheet data", "https://docs.google.com/spreadsheets/d/1Wi1Cg0uHVHaEwUeSRae2neZoD93dYVkoHmjdqvDd9Ko/edit?usp=sharing");
            infoDifficulty.ChangeTitle("Useful patches info");
            infoDifficulty.ChangeText(tempText);
            infoDifficulty.ShowDialog();
        }

        private void InfoTechniques_Click(object sender, EventArgs e)
        {
            string tempText = "These are patches related to techniques:" + Environment.NewLine + Environment.NewLine +
               "- Better learn tech battle: this will improve the chances of learning a technique after a battle" + Environment.NewLine + Environment.NewLine +
               "- Better learn tech brains: this will improve the chances of learning a technique while training brains" + Environment.NewLine + Environment.NewLine +
               "- Learn multiple techs: this patch allows a player to learn more than one technique after a battle";

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
        }

        private void Vermillimon_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetVermillimon(Vermillimon.Checked);
        }

        private void MyotismonInfo_Click(object sender, EventArgs e)
        {
            string tempText = "This patch will exchange Machinedramon with Myotismon as a playable partner:" + Environment.NewLine + Environment.NewLine +
               "- Myotismon will has 16 techs and will use 'Dark Thunder' as a finisher. It has the Wind/Ice/Battle types in that order." + Environment.NewLine + Environment.NewLine +
               "- Dark Thunder: Air type, 250 power" + Environment.NewLine + Environment.NewLine +
               "- Its boosted technique is 'Hurricane' with 700 potency" + Environment.NewLine + Environment.NewLine +
               "- Myotismon can enter the normal cups as well as the zero, cool, humanoid and wind cup" + Environment.NewLine + Environment.NewLine +
               "- It has the same requirements as Machinedramon, and mostly the same behaviour" + Environment.NewLine + Environment.NewLine +
               "- It has the same stat gains as Machinedramon and uses the Devil Coder as its evolution item" + Environment.NewLine + Environment.NewLine +
               "- Factorial Town will have the following changes:" + Environment.NewLine + Environment.NewLine +
               " * Devimon now has a 50% chance to evolve to Myotismon" + Environment.NewLine + Environment.NewLine +
               " * Greymon now has a 10% chance to evolve to Myotismon" + Environment.NewLine + Environment.NewLine +
               " * Seadramon now has a 10% chance to evolve to Mamemon";

            ViceHackInfo infoDifficulty = new ViceHackInfo();
            infoDifficulty.ChangeLink("Myotismon spreadsheet data", "https://docs.google.com/spreadsheets/d/17-Xsd4rshCH_MenzfCeV3TdKIFyfvxLPL4Q1eebMezQ/edit?gid=1550903355#gid=1550903355");
            infoDifficulty.ChangeTitle("Myotismon patch info");
            infoDifficulty.ChangeText(tempText);
            infoDifficulty.ShowDialog();
        }

        private void CurlingRandomizerInfo_Click(object sender, EventArgs e)
        {
            string tempText = "The curling randomizer makes you play with a random opponent when playing against Penguinmon. Rewards are still the same, the AI is also the same." + Environment.NewLine + Environment.NewLine +
            "Metal Mamemon matches are not afected by the randomizer, but playing against Penguinmon may make Metal Mamemon show up. " + Environment.NewLine + Environment.NewLine +
            "The randomizer includes all the digimon you have as a partner or you can battle against plus Jijimon, Market Manager, Shogun Gekkomon, King Sukamon, Analogman, Hagurumon and Tinmon." + Environment.NewLine + Environment.NewLine +
            "Some digimon will have a shorter name (such as 'platinumsukamo') due to the limitations the name displayed has, it has a limit of 14 characters." + Environment.NewLine + Environment.NewLine +
            "Some digimon may be displaced due to the animation, the position will be reset after the turn comes back to you.";

            OptionalPatchesInfo infoWindow = new OptionalPatchesInfo();
            infoWindow.Text = "Info window";
            infoWindow.ChangeTitle("Curling randomizer info");
            infoWindow.ChangeText(tempText);
            infoWindow.ShowDialog();
        }

        private void UnlockAreas_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetUnlockAreas(UnlockAreas.Checked);
        }

        private void deRandoFact_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetDeRandoFact(deRandoFact.Checked);
        }

        private void randoAdaptor_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetRandoCompatible(randoAdaptor.Checked);
        }

        private void insaneBattle_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetInsaneBattle(insaneBattle.Checked);
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

        private void restoreChainMelon_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetRestoreMelon(restoreChainMelon.Checked);
        }

        private void removeTechBoost_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetRemoveTechBoost(removeTechBoost.Checked);
        }

        private void restoreLifetime_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetRestoreLifetime(restoreLifetime.Checked);
        }

        public void EndPatchingApp(object sender, EventArgs e)
        {
            parentForm.patchFinished -= EndPatchingApp;
            tempConfirm.FormClosed += new FormClosedEventHandler(CloseApp);
            tempConfirm.ConfirmExit();            
        }

        void CloseApp(object sender, EventArgs e) { this.Close(); }

        private void colourFix_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetMapColour(colourFix.Checked);
        }
    }
}
