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
            string tempText = "Version 1.0.4" + Environment.NewLine + Environment.NewLine +
                "Vice hack version of the hardcore hack. Applying this patch will disable the 'super useful' and 'ultra lucky' useful patches" + Environment.NewLine + Environment.NewLine +
                "It will increase the difficulty of the game by modifying the stats and moveset of the NPC in the game and add some new content." + Environment.NewLine + Environment.NewLine +
                "Some of the features are:" + Environment.NewLine + Environment.NewLine +
                "- More stats for all the NPC digimon" + Environment.NewLine + Environment.NewLine +
                "- Different movesets for the NPC digimon (such as removing slow weak techs like 'static elec')." + Environment.NewLine + Environment.NewLine +
                "- New boss battles" + Environment.NewLine + Environment.NewLine +
                "- NPCs will now have the tech boost feature" + Environment.NewLine + Environment.NewLine +
                "- 1.0.3 addition: The bonus try is now rigged again and it will force you to fail, if you want to unrig it, you can use the optional installer." + Environment.NewLine + Environment.NewLine +
                "- 1.0.4 addition: The debug mode will not be friendly.";


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
               "- Ultra hardcore mode: you will be unable to run from battle and unable to use items in battle, on top of that, the tech boost for your digimon will be disabled and all of the NPCs will have a tech boost applied to all the normal techs" + Environment.NewLine + Environment.NewLine +
               "- Progression mode: technically a main patch but it can be mixed, click on the question mark next to it for detailed info" + Environment.NewLine + Environment.NewLine + Environment.NewLine +
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
               "- Starters 2: Changes the starters to Palmon, Biyomon, Patamon and Penguinmon" + Environment.NewLine + Environment.NewLine +
               "- Kunemon start: All the startes choices will give you a Kunemon." + Environment.NewLine + Environment.NewLine +
               "- Restore Panjyamon: Removes Weregarurumon and puts back Panjyamon, any change related to this is undone." + Environment.NewLine + Environment.NewLine +
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
               "Removed due to the differences between vanilla and this hack" + Environment.NewLine + Environment.NewLine + Environment.NewLine +
               "OTHER PATCHES" + Environment.NewLine + Environment.NewLine +
               "- Insane battles: this patch makes the back dimension battles work in a weird way" + Environment.NewLine + Environment.NewLine +
               "- Fully unlock areas: this patch will let you enter the Mansion, Sanctuary and Toy Town without any restrictions" + Environment.NewLine + Environment.NewLine +
               "- Remove tech boost: this patch will remove the tech boost of all digimon (not available in challenge or ultra hardcore)" + Environment.NewLine + Environment.NewLine +
               "- Restore ultimate extended lifetime: The extended lifetime of 8 days when becoming an ultimate, will go back to the original 4 days" + Environment.NewLine + Environment.NewLine +
               "- Map colour break: It breaks the colour of two extra maps, making them look unpleasant." + Environment.NewLine + Environment.NewLine +
               "- Remove telephone: Removes the collision for the telephone in the Ogre Fortress (why did I even make this?).";


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
               "- Better drops: Digimon have better chances to drop an item, some digimon now drop different items" + Environment.NewLine + Environment.NewLine +
               "- Better restaurant: the restaurant will give better stats, check the spreadsheet for details" + Environment.NewLine + Environment.NewLine +
               "- Better drimogemon: Drimogemon will give better rewards and lower the time it requires to find treasure, check the spreadsheet for details" + Environment.NewLine + Environment.NewLine +
               "- Better cards: Rare cards will be more frequent and the card merit value will be higher, check the spreadsheet for details." + Environment.NewLine + Environment.NewLine +
               "- Better merit: The merit shop will have more items and better prices, check the spreadsheet for details." + Environment.NewLine + Environment.NewLine +
               "- Better fishing: Fishing will become easier, check the spreadsheet for details." + Environment.NewLine + Environment.NewLine +
               "- Better item spawns: The items that randomly spawn will be more frequent, check the spreadsheet for details." + Environment.NewLine + Environment.NewLine +
               "- Better raise: Raising a digimon will be easier (evolution requirements are not affected), check the spreadsheet for details." + Environment.NewLine + Environment.NewLine +
               "- Better curling: The curling rewards will be better, check the spreadsheet for details." + Environment.NewLine + Environment.NewLine +
               "- Useful items 2: Kuwagamon and Kabuterimon will sell items at the Gym." + Environment.NewLine + Environment.NewLine +
               "- Training boost fix: This will make food not override the training boost, this means you can eat a meat after a supercarrot and you will still keep the boost to your training.";

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
               "- Learn multiple techs: this patch allows a player to learn more than one technique after a battle" + Environment.NewLine + Environment.NewLine +
               "- Nerf Ice Statue: this patch will lower Ice Statue power, accuracy and chance of trigger stun" + Environment.NewLine + Environment.NewLine +
               "- Insane Tech damage: this patch will change the range of the techniques damage from '90% - 110%' to '10% - 190%', finishers are not affected by this patch.";

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
               "- Its boosted technique is 'Megalo Spark' with 750 potency" + Environment.NewLine + Environment.NewLine +
               "- Myotismon can enter the normal cups as well as the zero, cool, humanoid and wind cup" + Environment.NewLine + Environment.NewLine +
               "- It has it's own requirements and raise, check the spreadsheet for details" + Environment.NewLine + Environment.NewLine +
               "- It has it's own stat gains" + Environment.NewLine + Environment.NewLine +
               "- Factorial Town will have the following changes:" + Environment.NewLine + Environment.NewLine +
               " * Devimon now has a 50% chance to evolve to Myotismon" + Environment.NewLine + Environment.NewLine +
               " * Greymon now has a 10% chance to evolve to Myotismon" + Environment.NewLine + Environment.NewLine +
               " * Seadramon now has a 10% chance to evolve to Mamemon";

            ViceHackInfo infoDifficulty = new ViceHackInfo();
            infoDifficulty.ChangeLink("Myotismon spreadsheet data", "https://docs.google.com/spreadsheets/d/1lG3aLJsLiCwcZXo5-OS18o21GngTVuyAiKA0liV_kpM/edit?gid=1719894242#gid=1719894242");
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
            parentForm.SetMapColour(colourBreak.Checked);
        }

        private void Starters2_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetStarters2(Starters2.Checked);

            if (Starters2.Checked)
                Kunemon.Checked = false;
        }

        private void Kunemon_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetKunemon(Kunemon.Checked);
            if (Kunemon.Checked)
                Starters2.Checked = false;
        }

        private void Panjyamon_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetRestorePanjya(Panjyamon.Checked);
        }

        private void ProgressionMode_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetProgression(ProgressionMode.Checked);
            if (ProgressionMode.Checked)
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

        private void NerfIce_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetIceNerf(NerfIce.Checked);
        }

        private void RightButton_Click(object sender, EventArgs e)
        {
            LeftButton.Enabled = true;
            LeftButton.Visible = true;
            betterDrops.Visible = true;
            betterDrops.Enabled = true;
            BetterDrimogemon.Visible = true;
            BetterDrimogemon.Enabled = true;
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
            trainingBoost.Visible = true;
            trainingBoost.Enabled = true;


            RightButton.Enabled = false;
            RightButton.Visible = false;
            statsGains.Enabled = false;
            statsGains.Visible = false;
            sStatsGains.Visible = false;
            uStatsGains.Visible = false;
            statsGains.Enabled = false;
            uStatsGains.Enabled = false;
            evoItem.Enabled = false;
            evoItem.Visible = false;
            ShortIntro.Visible = false;
            ShortIntro.Enabled = false;
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
        }

        private void LeftButton_Click(object sender, EventArgs e)
        {
            RightButton.Enabled = true;
            RightButton.Visible = true;
            statsGains.Enabled = true;
            statsGains.Visible = true;
            sStatsGains.Visible = true;
            uStatsGains.Visible = true;
            statsGains.Enabled = true;
            uStatsGains.Enabled = true;
            evoItem.Enabled = true;
            evoItem.Visible = true;
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

            if (!Hardcore.Checked)
            {
                sBonusTry.Enabled = true;
                uBonustry.Enabled = true;
            }

            uBonustry.Visible = true;           
            sBonusTry.Visible = true;

            LeftButton.Enabled = false;
            LeftButton.Visible = false;
            betterDrops.Visible = false;
            betterDrops.Enabled = false;
            BetterDrimogemon.Visible = false;
            BetterDrimogemon.Enabled = false;
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
            trainingBoost.Visible = false;
            trainingBoost.Enabled = false;

        }

        private void UsefulItems2_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetUseful2(UsefulItems2.Checked);
        }

        private void BetterRaise_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetRaise(BetterRaise.Checked);
        }

        private void BetterItemSpawns_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetItemSpawns(BetterItemSpawns.Checked);
        }

        private void BetterFishing_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetFishing(BetterFishing.Checked);
        }

        private void BetterCurling_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetCurling(BetterCurling.Checked);
        }

        private void BetterDrimogemon_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetDrimogemon(BetterDrimogemon.Checked);
        }

        private void BetterMerit_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetMerit(BetterMerit.Checked);
        }

        private void BetterCards_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetCards(BetterCards.Checked);
        }

        private void BetterRestaurant_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetRestaurant(BetterRestaurant.Checked);
        }

        private void TrainingBoost_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetTrainingBoost(trainingBoost.Checked);
        }

        private void insaneTechs_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetInsaneDamage(insaneTechs.Checked);
        }

        private void removeTelephone_CheckedChanged(object sender, EventArgs e)
        {
            parentForm.SetTelephone(removeTelephone.Checked);
        }
    }
}
