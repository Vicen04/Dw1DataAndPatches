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
    public partial class MainApp : Form
    {
        string filePath, newFilePath, fileDirectory;
        System.IO.BinaryReader ppf;
        System.IO.Stream bin;   
        public EventHandler patchFinished;

        public enum patchType
        {
            VICEHACK = 0,
            HARDCORE = 1,
            NONE = 2   
        }

        public enum viceDifficulty
        {
            CHALLENGE = 0,
            HARDCORE = 1,
            HARDMODE = 2,
            NONE = 3,
        }

        viceDifficulty currentViceDifficulty;
        patchType currentPatcher;

        bool myotismon , vermillimon , filth, restoreFilth, hardmode, superHardcore, hardMono, hardTourney, infinityBurn, betterBattleTech, betterBrainTechs, betterDrop, superBonus,
             ultraBonus, dirtReduction, sDirtReduction, shortIntro, statsGains, sStatsGains, uStatsGains, multipleTechs, evoItem, helpfulItems, lessMono, 
             BGMpatch, curlingRandomizer;

        //Vice exclusive
        bool insaneBattle, restoreMelon, restoreLifetime, removeTechBoost, deRandoFactTown, randoCompatible, unlockAreas, mapColour, ultraHardcore;

        //bug fixes vanilla
        bool battleText, battleTime, bonusTry, softlock, mojyamon, tourneySchedule, saveData, tankmon, textboxChoice, missingText, forgetMoves, giromon, rotation,
             evoTarget, sleepHunger, bankText, omniDisk, MPtext, sleepRegen, Digitamamon, evoItemReject, evoItemFlag, Sukamon, prosperity, recycling;

        public MainApp()
        {
            RestartBools();
            InitializeComponent();
        }

        private void ViceHackInfo_Click(object sender, EventArgs e)
        {
            Form viceHackInfo = new ViceHackInfo();            
            viceHackInfo.ShowDialog();
        }

        private void OptionalPatchesInfo_Click(object sender, EventArgs e)
        {
            Form optionalPatchesInfo = new OptionalPatchesInfo();
            optionalPatchesInfo.ShowDialog();
        }

        private void HardcoreInfo_Click(object sender, EventArgs e)
        {
            Form hardcoreHackInfo = new HardcoreHackInfo();
            hardcoreHackInfo.ShowDialog();
        }

        private void VicePatchButton_Click(object sender, EventArgs e)
        {
            currentPatcher = patchType.VICEHACK;
            Form vicePatcher = new ViceHackPatcher(this);
            vicePatcher.ShowDialog();
        }

        private void HardcorePatcher_Click(object sender, EventArgs e)
        {
            currentPatcher = patchType.HARDCORE;
            Form harcorePatcher = new HardcorePatcher(this);
            harcorePatcher.ShowDialog();
        }

        private void OptionalPatcher_Click(object sender, EventArgs e)
        {
            currentPatcher = patchType.NONE;
            Form optionalPatcher = new HardcorePatcher(this);
            optionalPatcher.ShowDialog();
        }

        public void RestartBools(object sender = null, EventArgs e = null)
        {
            myotismon = vermillimon = filth = restoreFilth = hardmode = superHardcore = hardMono = hardTourney = infinityBurn = betterBattleTech = betterBrainTechs = betterDrop 
            = superBonus = ultraBonus = dirtReduction = sDirtReduction = shortIntro = statsGains = sStatsGains = uStatsGains = multipleTechs = evoItem 
            = helpfulItems = lessMono = BGMpatch = curlingRandomizer = false;

            //Vice exclusive
            insaneBattle = restoreMelon = restoreLifetime = removeTechBoost = deRandoFactTown = randoCompatible = unlockAreas = mapColour = ultraHardcore = false;

            //bug fixes vanilla
            battleText = battleTime = bonusTry = softlock = mojyamon = tourneySchedule = saveData = tankmon = textboxChoice = missingText = forgetMoves = giromon 
            = rotation = evoTarget = sleepHunger = bankText = omniDisk = MPtext = sleepRegen = Digitamamon = evoItemReject = evoItemFlag = Sukamon = prosperity = recycling = false;

            currentPatcher = patchType.NONE;
            currentViceDifficulty = viceDifficulty.NONE;

            if (patchFinished != null)
            foreach (EventHandler d in patchFinished.GetInvocationList())
            {
                patchFinished -= d;
            }
        }


        public void InstallVicePatch() 
        {
            string path = System.IO.Directory.GetCurrentDirectory() + "/Patches/ViceHack/";
            //main patch
            SetPatch(path + "DigimonWorldVice_1.11.2.ppf");

            //optional exclusive patches
            switch (currentViceDifficulty)
            {
                case viceDifficulty.CHALLENGE:
                    SetPatch(path + "ChallengePatch.ppf");
                    break;
                case viceDifficulty.HARDMODE:
                    SetPatch(path + "DigimonWorldViceHardmode.ppf");
                    break;
                case viceDifficulty.HARDCORE:
                    SetPatch(path + "HardcoreVice 1.0.1.ppf");                    
                    break;
                default:
                    break;
            }

            if (ultraHardcore)
                SetPatch(path + "UltraHardcoreEnabler.ppf");

            if (curlingRandomizer)
                SetPatch(path + "CurlingRandomizerViceHack.ppf");            

            if (myotismon)
                SetPatch(path + "MyotismonPatchVice.ppf");

            if (filth)
                SetPatch(path + "FilthChallengeViceHack 1.1.3.ppf");

            if (insaneBattle)
                SetPatch(path + "InsaneBattle.ppf");

            if (restoreLifetime)
                SetPatch(path + "RemoveExtraLifetimeEvo.ppf");

            if (restoreMelon)
                SetPatch(path + "RestoreChainMelon.ppf");

            if (removeTechBoost)
                SetPatch(path + "RemoveTechBoost.ppf");

            if (deRandoFactTown)
                SetPatch(path + "DerandomizeFactorialTown.ppf");

            if (randoCompatible)
                SetPatch(path + "RandomizerCompatibilityPatch.ppf");

            if (unlockAreas)
                SetPatch(path + "FullyUnlockAreas.ppf");

            if (mapColour)
                SetPatch(path + "MapColourFix.ppf");

            InstallOptionalPatches();
        }

        public void InstallHardcore()
        {
            //main patch
            SetPatch(System.IO.Directory.GetCurrentDirectory() + "/Patches/Hardcore/DigimonWorldHardcore 1.0.1.ppf");
            //Run other patches
            ExtraInstall();
        }

        public void ExtraInstall()
        {
            InstallBugFixes();
            InstallOptionalPatches();
        }

        void InstallOptionalPatches()
        {
            string path = System.IO.Directory.GetCurrentDirectory() + "/Patches/OptionalPatches/";

            if (myotismon && currentPatcher != patchType.VICEHACK)
                SetPatch(path + "MyotismonMaeson.ppf");

            if (curlingRandomizer && currentPatcher != patchType.VICEHACK)
                SetPatch(path + "DigimonWorldCurlingRandomizer 1.1.ppf");

            if (filth)
            {
                if (currentPatcher != patchType.VICEHACK)
                    SetPatch(path + "Difficulty patches/FilthChallengePatch 1.1.1.ppf");

                if (restoreFilth)
                    SetPatch(path + "Difficulty patches/RestoreFilthDigimon.ppf");
            }

            if (vermillimon)
                SetPatch(path + "MonochromonToVermilimon.ppf");

            if (hardmode)
                SetPatch(path + "Difficulty patches/DigimonWorldHarderBossesVice.ppf");

            if (superHardcore)
                SetPatch(path + "Difficulty patches/SuperHardcoreEnabler.ppf");

            if (hardMono)
                SetPatch(path + "Difficulty patches/IncreaseMonochromonGoal.ppf");

            if (hardTourney)
                SetPatch(path + "Difficulty patches/MoreDifficultTournaments.ppf");

            if (infinityBurn)
                SetPatch(path + "Tech patches/BetterInfinityBurn.ppf");

            if (betterBattleTech)
                SetPatch(path + "Tech patches/BetterBattleTechChances.ppf");

            if (betterBrainTechs)
                SetPatch(path + "Tech patches/BetterBrainsTechChances.ppf");

            if (multipleTechs)
                SetPatch(path + "Tech patches/LearnMoreTechs.ppf");

            if (betterDrop)
                SetPatch(path + "Useful patches/BetterDrops.ppf");

            if (bonusTry)
            {
                SetPatch(path + "Useful patches/BonusTryFix 1.1.ppf");

                if (superBonus)
                    SetPatch(path + "Useful patches/BonusTrySuperHelpful 1.1.ppf");
                else if (ultraBonus)
                    SetPatch(path + "Useful patches/BonusTryUltraLucky 1.1.ppf");
            }

            if (dirtReduction)
                SetPatch(path + "Useful patches/DirtReductionHalf.ppf");
            else if (sDirtReduction)
                SetPatch(path + "Useful patches/SuperDirtReduction.ppf");

            if (shortIntro)
                SetPatch(path + "Useful patches/ShortIntro.ppf");

            if (statsGains)
                SetPatch(path + "Useful patches/NewStatsGains.ppf");
            else if (sStatsGains)
                SetPatch(path + "Useful patches/NewStatsGainsSuper.ppf");
            else if (uStatsGains)
                SetPatch(path + "Useful patches/NewStatsGainsHyper.ppf");

            if (evoItem)
                SetPatch(path + "Useful patches/EnableStatGainsEvoItems.ppf");

            if (helpfulItems)
                SetPatch(path + "Useful patches/HelpfulItems.ppf");

            if (lessMono)
                SetPatch(path + "Useful patches/LowerMonochromonBits.ppf");

            if (BGMpatch)
                SetPatch(path + "Useful patches/BGMPatch.ppf");

            if (unlockAreas && patchType.VICEHACK != currentPatcher)
                SetPatch(path + "Useful patches/DigimonWorldUnlockAreasVice_fixed.ppf");


            bin.Close();
            bin.Dispose();
            CreateTxt();
            if (patchFinished != null) patchFinished(this, EventArgs.Empty);
        }

        void InstallBugFixes()          
        {
            string path = System.IO.Directory.GetCurrentDirectory() + "/Patches/BugFixPatches/";

            if (battleText)
                SetPatch(path + "BattleTextFix.ppf");

            if (battleTime)
                SetPatch(path + "BattleTimeFix.ppf");

            if (softlock)
                SetPatch(path + "SomeSoftlockFixes.ppf");

            if (mojyamon)
                SetPatch(path + "MoyjamonFix.ppf");

            if (tourneySchedule)
                SetPatch(path + "TournamentScheduleTextFix.ppf");

            if (saveData)
                SetPatch(path + "SaveFileTextPatch.ppf");

            if (tankmon)
                SetPatch(path + "TankmonTechFix.ppf");

            if (textboxChoice)
                SetPatch(path + "TextboxChoiceFix.ppf");

            if (missingText)
                SetPatch(path + "VariousTextsFixed.ppf");

            if (forgetMoves)
                SetPatch(path + "ForgetMovesFix.ppf");

            if (giromon)
                SetPatch(path + "GiromonJukeboxFix 1.1.ppf");

            if (rotation)
                SetPatch(path + "RotationFix.ppf");

            if (evoTarget)
                SetPatch(path + "GetEvolutionTargetFix.ppf");

            if (sleepHunger)
                SetPatch(path + "SleepHungerErrorFix.ppf");

            if (bankText)
                SetPatch(path + "BankTextFix.ppf");

            if (omniDisk)
                SetPatch(path + "OmniDiskFix.ppf");

            if (MPtext)
                SetPatch(path + "MPConsumptionFix.ppf");

            if (sleepRegen)
                SetPatch(path + "SleepRegenFix.ppf");

            if (Digitamamon)
                SetPatch(path + "DigitamamonRestaurantFix.ppf");

            if (evoItemReject)
                SetPatch(path + "EvoItemRejectionFix.ppf");

            if (evoItemFlag)
                SetPatch(path + "EvoItemFlagFix.ppf");

            if (Sukamon)
                SetPatch(path + "SukamonStatGainsFix.ppf");

            if (prosperity)
                SetPatch(path + "Jijimon Prosperity Fix.ppf");

            if (recycling)
                SetPatch(path + "RecyclingTextFix.ppf");

        }

        public void SetMyotismon(bool enabled) { myotismon = enabled; }
        public void SetViceDifficulty(viceDifficulty difficulty) { currentViceDifficulty = difficulty; }
        public void SetFilePath(string path) { filePath = path; }
        public void SetFilth(bool enabled) { filth = enabled; }
        public void SetRestoreFilth(bool enabled) { restoreFilth = enabled; }
        public void SetVermillimon (bool enabled) { vermillimon = enabled; }
        public void SetHardmode(bool enabled) { hardmode = enabled; }
        public void SetSuperHardcore(bool enabled) { superHardcore = enabled; }
        public void SetHardMono(bool enabled) { hardMono = enabled; }
        public void SetHardTourney(bool enabled) { hardTourney = enabled; }
        public void SetInfinityBurn(bool enabled) {infinityBurn = enabled; }
        public void SetBetterBattleTech(bool enabled) { betterBattleTech = enabled; }
        public void SetBetterBrainTech(bool enabled) { betterBrainTechs = enabled; }
        public void SetBetterDrops(bool enabled) { betterDrop = enabled; }
        public void SetSuperBonus(bool enabled) { superBonus = enabled; }
        public void SetUltraBonus(bool enabled) { ultraBonus = enabled; }
        public void SetSDirtReduction(bool enabled) { sDirtReduction = enabled; }
        public void SetDirtReduction(bool enabled) { dirtReduction = enabled; }
        public void SetMapColour(bool enabled) { mapColour = enabled; }
        public void SetShortIntro(bool enabled) { shortIntro = enabled; }
        public void SetStatsGains(bool enabled) { statsGains = enabled; }
        public void SetSStatsGains(bool enabled) { sStatsGains = enabled; }
        public void SetUStatsGains(bool enabled) { uStatsGains = enabled; }
        public void SetMultipleTechs(bool enabled) { multipleTechs = enabled; }
        public void SetEvoItem(bool enabled) { evoItem = enabled; }
        public void SetHelpfulItems(bool enabled) { helpfulItems = enabled; }
        public void SetLessMono(bool enabled) { lessMono = enabled; }
        public void SetBGMPatch (bool enabled) { BGMpatch = enabled; }
        public void SetCurlingRandomizer(bool enabled) { curlingRandomizer = enabled; }
        public void SetInsaneBattle(bool enabled) { insaneBattle = enabled; }
        public void SetRestoreMelon(bool enabled) { restoreMelon = enabled; }
        public void SetRestoreLifetime(bool enabled) { restoreLifetime = enabled; }
        public void SetRemoveTechBoost(bool enabled) { removeTechBoost = enabled; }
        public void SetDeRandoFact(bool enabled) { deRandoFactTown = enabled; }
        public void SetRandoCompatible(bool enabled) { randoCompatible = enabled; }
        public void SetUnlockAreas (bool enabled) { unlockAreas = enabled; }
        public void SetGiromon(bool enabled) { giromon = enabled; }
        public void SetBattleText(bool enabled) { battleText = enabled; }
        public void SetBattleTime(bool enabled) { battleTime = enabled; }
        public void SetBonusTry(bool enabled) { bonusTry = enabled; }
        public void SetSoftlocks(bool enabled) { softlock = enabled; }
        public void SetMojyamon(bool enabled) { mojyamon = enabled; }
        public void SetTourney(bool enabled) { tourneySchedule = enabled; }
        public void SetSaveData(bool enabled) { saveData = enabled; }
        public void SetTankmon(bool enabled) { tankmon = enabled; } 
        public void SetTextboxChoice(bool enabled) { textboxChoice = enabled; }
        public void SetMissingText(bool enabled) { missingText = enabled; } 
        public void SetForgetMoves(bool enabled) { forgetMoves = enabled; }
        public void SetRotation(bool enabled) { rotation = enabled; }
        public void SetEvoTarget(bool enabled) { evoTarget = enabled; }
        public void SetSleepHunger(bool enabled) { sleepHunger = enabled; }
        public void SetSleepRegen(bool enabled) { sleepRegen = enabled; }
        public void SetBankText(bool enabled) { bankText = enabled; }
        public void SetMPText(bool enabled) { MPtext = enabled; }
        public void SetOmniDisk(bool enabled) { omniDisk = enabled; }
        public void SetDigitamamon(bool enabled) { Digitamamon = enabled; }
        public void SetItemReject(bool enabled) { evoItemReject = enabled; }
        public void SetEvoItemFlag(bool enabled) { evoItemFlag = enabled; }
        public void SetSukamon(bool enabled) { Sukamon = enabled; }
        public void SetProsperity(bool enabled) { prosperity = enabled; }
        public void SetUltraHardcore(bool enabled) { ultraHardcore = enabled; }
        public void SetRecycling(bool enabled) { recycling = enabled; }
        public patchType GetPatchType() { return currentPatcher; }


        public void CreatePatchedFile(string folderDestination, string newFilename)
        {
            fileDirectory = folderDestination;
            newFilePath = System.IO.Path.Combine(folderDestination, newFilename);
            newFilePath = newFilePath + System.IO.Path.GetExtension(filePath);
            System.IO.File.Copy(filePath, newFilePath , true);
            bin = System.IO.File.OpenWrite(newFilePath);

            switch(currentPatcher)
            {
                case patchType.VICEHACK:
                    InstallVicePatch();
                    break;
                case patchType.HARDCORE:
                    InstallHardcore();
                    break;
                case patchType.NONE:
                    ExtraInstall();
                    break;
            }
        }



        //Adapted from the Icarus/Paradox code
        void SetPatch(string patchPath)
        {            
            ppf = new System.IO.BinaryReader(System.IO.File.Open(patchPath, System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite, System.IO.FileShare.ReadWrite));
            ApplyPPF3Patch();
        }

        void ApplyPPF3Patch()
        {
            ppf.BaseStream.Position = 60;
            int offset, change;
            

            while (ppf.BaseStream.Position < ppf.BaseStream.Length)
            {
                offset = ppf.ReadInt32();
                ppf.ReadInt32();
                change = ppf.ReadByte();
                bin.Position = offset;
                
                while (change > 0)
                {
                    bin.WriteByte(ppf.ReadByte());
                    change--;
                }
               
            }
            ppf.Close();
            ppf.Dispose();
        }

        void CreateTxt()
        {
            string filename = "";

            switch (currentPatcher)
            {
                case patchType.VICEHACK:
                    filename = "Vice hack patches.txt";
                    break;
                case patchType.HARDCORE:
                    filename = "Hardcore hack patches.txt";
                    break;
                case patchType.NONE:
                    filename = "Patches installed.txt";
                    break;
            }
            string path = System.IO.Path.Combine(fileDirectory, filename);
            System.IO.Stream txt = System.IO.File.OpenWrite(path);

            System.IO.StreamWriter txtWritter = new System.IO.StreamWriter(txt);            

            switch (currentPatcher)
            {
                case patchType.VICEHACK:
                    txtWritter.Write("Vice hack main patch");
                    txtWritter.WriteLine();
                    txtWritter.WriteLine();
                    txtWritter.Write("Difficulty patches:");
                    txtWritter.WriteLine();
                    switch (currentViceDifficulty)
                    {
                        case viceDifficulty.CHALLENGE:
                            txtWritter.Write("- Challenge patch");
                            break;
                        case viceDifficulty.HARDMODE:
                            txtWritter.Write("- Vice hardmode");
                            break;
                        case viceDifficulty.HARDCORE:
                            txtWritter.Write("- Vice hardcore");
                            break;
                        default:
                            break;
                    }               
                    break;
                case patchType.HARDCORE:
                    txtWritter.Write("Hardcore hack main patch");
                    txtWritter.WriteLine();
                    txtWritter.WriteLine();
                    txtWritter.Write("Difficulty patches:");
                    break;
                case patchType.NONE:
                    txtWritter.Write("Difficulty patches:");
                    break;
            }

            txtWritter.WriteLine();

            if (filth)
            {
                if (currentPatcher == patchType.VICEHACK)
                    txtWritter.Write("- Vice Filth challenge");
                else
                    txtWritter.Write("- Filth challenge");
                txtWritter.WriteLine();
            }

            if (hardmode)
            {
                txtWritter.Write("- Harder bosses");
                txtWritter.WriteLine();
            }

            if (superHardcore)
            {
                txtWritter.Write("- Fair battles");
                txtWritter.WriteLine();
            }

            if (hardMono)
            {
                txtWritter.Write("- Hardcore Monochromon");
                txtWritter.WriteLine();
            }

            if (hardTourney)
            {
                txtWritter.Write("- Hardcore tournaments");
                txtWritter.WriteLine();
            }

            if (ultraHardcore)
            {                
                txtWritter.Write("- Ultra hardcore mode");
                txtWritter.WriteLine();               
            }

            txtWritter.WriteLine();
            txtWritter.Write("Digimon patches:");
            txtWritter.WriteLine();

            if (myotismon)
            {
                if(currentPatcher == patchType.VICEHACK)
                    txtWritter.Write("- Vice Myotismon");
                else
                    txtWritter.Write("- Myotismon for the Maeson hack");
                txtWritter.WriteLine();
            }

            if (curlingRandomizer)
            {
                if (currentPatcher == patchType.VICEHACK)
                    txtWritter.Write("- Vice curling randomizer");
                else
                    txtWritter.Write("- Curling randomizer");
                txtWritter.WriteLine();
            }

            if (vermillimon)
            {
                txtWritter.Write("- Vermillimon");
                txtWritter.WriteLine();
            }

            if (patchType.VICEHACK == currentPatcher)
            {
                txtWritter.WriteLine();
                txtWritter.Write("Miscellaneous patches:");
                txtWritter.WriteLine();

                if (randoCompatible)
                {
                    txtWritter.Write("- Randomizer compatibility");
                    txtWritter.WriteLine();
                }

                if (deRandoFactTown)
                {
                    txtWritter.Write("- Factorial Town fix for the randomizer");
                    txtWritter.WriteLine();
                }

                if (insaneBattle)
                {
                    txtWritter.Write("- Insane battle");
                    txtWritter.WriteLine();
                }

                if (restoreLifetime)
                {
                    txtWritter.Write("- Restore lifetime");
                    txtWritter.WriteLine();
                }

                if (restoreMelon)
                {
                    txtWritter.Write("- Restore chain melon");
                    txtWritter.WriteLine();
                }

                if (removeTechBoost)
                {
                    txtWritter.Write("- Remove tech boost");
                    txtWritter.WriteLine();
                }

                if (unlockAreas)
                {
                    txtWritter.Write("- Fully unlock areas");
                    txtWritter.WriteLine();
                }

                if (mapColour)
                {
                    txtWritter.Write("- Map colour fix");
                    txtWritter.WriteLine();
                }
            }

            txtWritter.WriteLine();
            txtWritter.Write("Useful patches:");
            txtWritter.WriteLine();           

            if (betterDrop)
            {
                txtWritter.Write("- Better drops");
                txtWritter.WriteLine();
            }

            if (bonusTry)
            {
                txtWritter.Write("- Bonus try");
                txtWritter.WriteLine();
            }

            if (superBonus)
            {
                txtWritter.Write("- Super useful rigging");
                txtWritter.WriteLine();
            }
            else if (ultraBonus)
            {
                txtWritter.Write("- Ultra lucky bonus try");
                txtWritter.WriteLine();
            }

            if (dirtReduction)
            {
                txtWritter.Write("- Drimogemon dirt reduction");
                txtWritter.WriteLine();
            }
            else if (sDirtReduction)
            {
                txtWritter.Write("- Super dirt reduction");
                txtWritter.WriteLine();
            }

            if (shortIntro)
            {
                txtWritter.Write("- Short intro");
                txtWritter.WriteLine();
            }

            if (statsGains)
            {
                txtWritter.Write("- New stats gains");
                txtWritter.WriteLine();
            }
            else if (sStatsGains)
            {
                txtWritter.Write("- Super stats gains");
                txtWritter.WriteLine();
            }
            else if (uStatsGains)
            {
                txtWritter.Write("- Ultra stats gains");
                txtWritter.WriteLine();
            }           

            if (evoItem)
            {
                txtWritter.Write("- Stat gains evo item");
                txtWritter.WriteLine();
            }

            if (helpfulItems)
            {
                txtWritter.Write("- Helpful items");
                txtWritter.WriteLine();
            }

            if (lessMono)
            {
                txtWritter.Write("- Super low Monochromon goal");
                txtWritter.WriteLine();
            }

            if (BGMpatch)
            {
                txtWritter.Write("- BGM patch");
                txtWritter.WriteLine();
            }

            txtWritter.WriteLine();
            txtWritter.Write("Techniques patches:");
            txtWritter.WriteLine();

            if (infinityBurn)
            {
                txtWritter.Write("- Better infinity burn");
                txtWritter.WriteLine();
            }

            if (betterBattleTech)
            {
                txtWritter.Write("- Better battle technique learn");
                txtWritter.WriteLine();
            }

            if (betterBrainTechs)
            {
                txtWritter.Write("- Better brains training technique learn");
                txtWritter.WriteLine();
            }

            if (multipleTechs)
            {
                txtWritter.Write("- Learn multiple techs");
                txtWritter.WriteLine();
            }

            if (patchType.VICEHACK != currentPatcher)
            {
                txtWritter.WriteLine();
                txtWritter.Write("Bug fixes:");
                txtWritter.WriteLine();

                if (bankText)
                {
                    txtWritter.Write("- Bank text fix");
                    txtWritter.WriteLine();
                }

                if (battleText)
                {
                    txtWritter.Write("- Battle text spam fix");
                    txtWritter.WriteLine();
                }

                if (battleTime)
                {
                    txtWritter.Write("- Battle time fix");
                    txtWritter.WriteLine();
                }

                if (evoTarget)
                {
                    txtWritter.Write("- Evolution target bug fix");
                    txtWritter.WriteLine();
                }

                if (evoItemFlag)
                {
                    txtWritter.Write("- Evolution item flag fix");
                    txtWritter.WriteLine();
                }

                if (evoItemReject)
                {
                    txtWritter.Write("- Evolution item rejection fix");
                    txtWritter.WriteLine();
                }

                if (forgetMoves)
                {
                    txtWritter.Write("- Forget techniques fix");
                    txtWritter.WriteLine();
                }

                if (missingText)
                {
                    txtWritter.Write("- Missing text fix");
                    txtWritter.WriteLine();
                }

                if (mojyamon)
                {
                    txtWritter.Write("- Mojyamon animations fix");
                    txtWritter.WriteLine();
                }

                if (omniDisk)
                {
                    txtWritter.Write("- OmniDisk fix");
                    txtWritter.WriteLine();
                }

                if (prosperity)
                {
                    txtWritter.Write("- Prosperity medal fix");
                    txtWritter.WriteLine();
                }

                if (recycling)
                {
                    txtWritter.Write("- Recycling shop text fix");
                    txtWritter.WriteLine();
                }

                if (rotation)
                {
                    txtWritter.Write("- Rotation bug fix");
                    txtWritter.WriteLine();
                }

                if (saveData)
                {
                    txtWritter.Write("- Save data text fix");
                    txtWritter.WriteLine();
                }

                if (sleepHunger)
                {
                    txtWritter.Write("- Sleep hunger fix");
                    txtWritter.WriteLine();
                }

                if (softlock)
                {
                    txtWritter.Write("- Some softlock fixes");
                    txtWritter.WriteLine();
                }

                if (tankmon)
                {
                    txtWritter.Write("- Tankmon missing tech fix");
                    txtWritter.WriteLine();
                }

                if (textboxChoice)
                {
                    txtWritter.Write("- Textbox choice fix");
                    txtWritter.WriteLine();
                }

                if (Digitamamon)
                {
                    txtWritter.Write("- Digitamamon restaurant exploit fix");
                    txtWritter.WriteLine();
                }

                if (giromon)
                {
                    txtWritter.Write("- Giromon jukebox fix");
                    txtWritter.WriteLine();
                }

                if (MPtext)
                {
                    txtWritter.Write("- MP consumption text freeze fix");
                    txtWritter.WriteLine();
                }       

                if (sleepRegen)
                {
                    txtWritter.Write("- Sleep regeneration bug fix");
                    txtWritter.WriteLine();
                }

                if (Sukamon)
                {
                    txtWritter.Write("- Sukamon natural evolution stats gains fix");
                    txtWritter.WriteLine();
                }

                if (tourneySchedule)
                {
                    txtWritter.Write("- Tournament schedule fix");
                    txtWritter.WriteLine();
                }
            }
            txtWritter.Close();
            txtWritter.Dispose();
            txt.Close();
            txt.Dispose();
        }
    }
}
