﻿using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.TurnBasedModifiers;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.Enums;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using System;
using System.Collections.Generic;
using System.Linq;
using Kingmaker.UnitLogic.Class.Kineticist;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.ElementsSystem;
using Kingmaker.Items;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.Utility;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Actions.Builder;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Abilities.Components.CasterCheckers;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.UnitLogic.Class.Kineticist.ActivatableAbility;
using Kingmaker.Items.Slots;
using Kingmaker.View.Equipment;
using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.Blueprints.JsonSystem;

namespace KineticArchetypes
{
    internal class KineticDuelist
    {
        internal const string ArchetypeName = "KineticDuelistArchetype";
        internal const string ArchetypeDisplayName = "KineticDuelist.Name";
        internal const string ArchetypeDescription = "KineticDuelist.Description";
        internal const string ArchetypeGuid = "179E8E47-35B8-48EF-84AE-10C6C0A067D3";

        internal const string ProficienciesDisplayName = "KineticDuelist.Proficiencies";
        internal const string ProficienciesDescription = "KineticDuelist.Proficiencies.Description";
        internal const string ProficienciesGuid = "B41F957B-5905-411E-B22F-DD6BED8BFB6E";

        internal const string KDKineticBladeName = "KineticDuelist.KineticBlade";
        internal const string KDKineticBladeGuid = "4C91D695-D84E-4AD5-AED4-0BDD3FD15CEA";
        internal const string KDKineticBladeDescription = "KineticDuelist.KineticBlade.Description";

        internal const string KineticDualBladesFeatureName = "KineticDuelist.KineticDualBladesFeature";
        internal const string KineticDualBladesFeatureGuid = "062A5F9E-493E-473D-B0D5-CA4CEAC0A29A";
        internal const string KineticDualBladesFeatureDescription = "KineticDuelist.KineticDualBladesFeature.Description";

        internal const string KineticDualBladesAbilityName = "KineticDuelist.KineticDualBladesAbility";
        internal const string KineticDualBladesAbilityGuid = "11EAB799-7E33-45B1-A006-5A48A9ADAB5E";
        internal const string KineticDualBladesAbilityDescription = "KineticDuelist.KineticDualBladesAbility.Description";

        internal const string KineticDualBladesBuffName = "KineticDuelist.KineticDualBladesBuff";
        internal const string KineticDualBladesBuffGuid = "93CB0B72-368D-4C07-B4DC-B9509719CD1F";
        internal const string KineticDualBladesBuffDescription = "KineticDuelist.KineticDualBladesBuff.Description";

        internal const string ImprovedKineticDualBladesName = "KineticDuelist.ImprovedKineticDualBlades";
        internal const string ImprovedKineticDualBladesGuid = "7DB4530F-7B21-4301-9F8D-99F405D1048D";
        internal const string ImprovedKineticDualBladesDescription = "KineticDuelist.ImprovedKineticDualBlades.Description";

        internal const string SynchronousChargeFeatureName = "KineticDuelist.SynchronousChargeFeature";
        internal const string SynchronousChargeFeatureGuid = "124E9629-F53D-48F3-BB69-308AF29F80F2";
        internal const string SynchronousChargeFeatureDescription = "KineticDuelist.SynchronousChargeFeature.Description";

        internal const string KineticAssaultFeatureName = "KineticDuelist.KineticAssaultFeature";
        internal const string KineticAssaultFeatureGuid = "74FDA391-BF63-4F36-8666-84DEBE7C70F2";
        internal const string KineticAssaultFeatureDescription = "KineticDuelist.KineticAssaultFeature.Description";
        internal const string KineticAssaultBuffName = "KineticDuelist.KineticAssaultBuff";
        internal const string KineticAssaultBuffGuid = "B2F8CEE0-E8AE-4EBA-9457-0F6A47068CD0";
        internal const string KineticAssaultBuffDescription = "KineticDuelist.KineticAssaultBuff.Description";

        internal const string KineticAssaultAbilityName = "KineticDuelist.KineticAssaultAbility";
        internal const string KineticAssaultAbilityGuid = "97380822-0A27-47A0-88D2-45115754D4F0";
        internal const string KineticAssaultAbilityDescription = "KineticDuelist.KineticAssaultAbility.Description";

        internal const string GreaterKineticDualBladesName = "KineticDuelist.GreaterKineticDualBlades";
        internal const string GreaterKineticDualBladesGuid = "E9D7BB7A-A4E4-4CE2-BC83-5A63F50CC869";
        internal const string GreaterKineticDualBladesDescription = "KineticDuelist.GreaterKineticDualBlades.Description";

        internal const string DualBlades2ndAttackBuffName = "KineticDuelist.DualBlades2ndAttackBuff";
        internal const string DualBlades2ndAttackBuffGuid = "2DC5E636-E9B7-402D-9D1F-0A821D836E32";
        internal const string DualBlades2ndAttackBuffDescription = "KineticDuelist.DualBlades2ndAttackBuff.Description";

        internal const string DualBlades3rdAttackBuffName = "KineticDuelist.DualBlades3rdAttackBuff";
        internal const string DualBlades3rdAttackBuffGuid = "44BE1F78-BCFD-4C4D-B6BA-874B725F5BDE";
        internal const string DualBlades3rdAttackBuffDescription = "KineticDuelist.DualBlades3rdAttackBuff.Description";

        internal const string SupressBladeBurnBuffName = "KineticDuelist.SupressBladeBurnBuff";
        internal const string SupressBladeBurnBuffGuid = "5DA2EB59-8D41-4D03-94FD-F7BFD8DD77F1";
        internal const string SupressBladeBurnBuffDescription = "KineticDuelist.SupressBladeBurnBuff.Description";

        internal static BlueprintAbilityReference[] allBlades;

        internal static readonly LogWrapper Logger = LogWrapper.Get("KineticArchetypes.KineticDuelist");

        internal static void Configure()
        {
            try
            {
                ConfigureEnabled();
            }
            catch (Exception e)
            {
                Logger.Error("Failed to configure kinetic duelist", e);
            }
        }

        private static void ConfigureEnabled()
        {
            Logger.Info($"Configuring {ArchetypeName}");

            allBlades = new BlueprintAbilityReference[]
            {
                AbilityRefs.KineticBladeAirBlastBurnAbility.Cast<BlueprintAbilityReference>().Reference,
                AbilityRefs.KineticBladeBlizzardBlastBurnAbility.Cast<BlueprintAbilityReference>().Reference,
                AbilityRefs.KineticBladeBloodBlastBurnAbility.Cast<BlueprintAbilityReference>().Reference,
                AbilityRefs.KineticBladeBlueFlameBlastBurnAbility.Cast<BlueprintAbilityReference>().Reference,
                AbilityRefs.KineticBladeChargeWaterBlastBurnAbility.Cast<BlueprintAbilityReference>().Reference,
                AbilityRefs.KineticBladeColdBlastBurnAbility.Cast<BlueprintAbilityReference>().Reference,
                AbilityRefs.KineticBladeEarthBlastBurnAbility.Cast<BlueprintAbilityReference>().Reference,
                AbilityRefs.KineticBladeElectricBlastBurnAbility.Cast<BlueprintAbilityReference>().Reference,
                AbilityRefs.KineticBladeFireBlastBurnAbility.Cast<BlueprintAbilityReference>().Reference,
                AbilityRefs.KineticBladeIceBlastBurnAbility.Cast<BlueprintAbilityReference>().Reference,
                AbilityRefs.KineticBladeMagmaBlastBurnAbility.Cast<BlueprintAbilityReference>().Reference,
                AbilityRefs.KineticBladeMetalBlastBurnAbility.Cast<BlueprintAbilityReference>().Reference,
                AbilityRefs.KineticBladeMudBlastBurnAbility.Cast<BlueprintAbilityReference>().Reference,
                AbilityRefs.KineticBladePlasmaBlastBurnAbility.Cast<BlueprintAbilityReference>().Reference,
                AbilityRefs.KineticBladeSandstormBlastBurnAbility.Cast<BlueprintAbilityReference>().Reference,
                AbilityRefs.KineticBladeSteamBlastBurnAbility.Cast<BlueprintAbilityReference>().Reference,
                AbilityRefs.KineticBladeThunderstormBlastBurnAbility.Cast<BlueprintAbilityReference>().Reference,
                AbilityRefs.KineticBladeWaterBlastBurnAbility.Cast<BlueprintAbilityReference>().Reference                
            };

            var archetype =
                ArchetypeConfigurator.New(ArchetypeName, ArchetypeGuid, CharacterClassRefs.KineticistClass)
                    .SetLocalizedName(ArchetypeDisplayName)
                    .SetLocalizedDescription(ArchetypeDescription);

            // Remove features
            archetype
                .AddToRemoveFeatures(1, FeatureRefs.KineticistProficiencies.ToString())
                .AddToRemoveFeatures(3, FeatureSelectionRefs.InfusionSelection.ToString())
                .AddToRemoveFeatures(9, FeatureSelectionRefs.InfusionSelection.ToString())
                .AddToRemoveFeatures(11, FeatureRefs.Supercharge.ToString())
                .AddToRemoveFeatures(13, FeatureRefs.MetakinesisQuickenFeature.ToString())
                ;

            // Add features
            var kdprof = ProficienciesFeature();
            var blade0 = KDKineticBladeFeature();
            var blade1 = KineticDualBladesFeature();
            var blade2 = ImprovedKineticDualBladesFeature();
            var synCha = SynchronousChargeFeature();
            var kinAss = KineticAssaultFeature();
            var blade3 = GreaterKineticDualBladesFeature();
            archetype
                .AddToAddFeatures(1, kdprof)
                .AddToAddFeatures(1, blade0)
                .AddToAddFeatures(3, blade1)
                .AddToAddFeatures(9, blade2)
                .AddToAddFeatures(11, synCha)
                .AddToAddFeatures(13, kinAss)
                .AddToAddFeatures(15, blade3)
                .Configure();
            var KineticistClass = CharacterClassRefs.KineticistClass.Reference.Get();
            
            // Create UI group for blades
            UIGroup uiGroup = new();
            uiGroup.Features.Add(blade0);
            uiGroup.Features.Add(blade1);
            uiGroup.Features.Add(blade2);
            uiGroup.Features.Add(blade3);
            var oldUIGroup = KineticistClass.Progression.UIGroups;
            int num = oldUIGroup.Length;
            var newUIGroup = new UIGroup[num + 1];
            if(num > 0)
                Array.Copy(oldUIGroup, newUIGroup, num);
            newUIGroup[num] = uiGroup;
            KineticistClass.Progression.UIGroups = newUIGroup;

            DualBlades2ndAttackBuff();
            DualBlades3rdAttackBuff();

            RestrictFormInfusionSelections();
        }

        private static BlueprintFeature ProficienciesFeature()
        {
            var proficiencies = new List<Blueprint<BlueprintUnitFactReference>> {
                FeatureRefs.SimpleWeaponProficiency.Cast<BlueprintUnitFactReference>().Reference,
                FeatureRefs.LightArmorProficiency.Cast<BlueprintUnitFactReference>().Reference,
                FeatureRefs.MediumArmorProficiency.Cast<BlueprintUnitFactReference>().Reference,
                FeatureRefs.BucklerProficiency.Cast<BlueprintUnitFactReference>().Reference,
                FeatureRefs.RayCalculateFeature.Cast<BlueprintUnitFactReference>().Reference,
            };

            return FeatureConfigurator.New(ProficienciesDisplayName, ProficienciesGuid)
              .SetDisplayName(ProficienciesDisplayName)
              .SetDescription(ProficienciesDescription)
              .SetIcon(FeatureRefs.MediumArmorProficiency.Reference.Get().Icon)
              .SetIsClassFeature(true)
              .AddFacts(proficiencies)
              .AddProficiencies(weaponProficiencies: new WeaponCategory[] { WeaponCategory.KineticBlast })
              .Configure();
        }

        private static BlueprintFeature KDKineticBladeFeature()
        {
            // Reduce kinetic blade cost by 1
            var reduceBladeCost = new AddKineticistBurnModifier
            {
                Value = -1,
                BurnType = KineticistBurnType.Infusion,
                m_AppliableTo = allBlades
            };

            return FeatureConfigurator.New(KDKineticBladeName, KDKineticBladeGuid)
                .SetDisplayName(KDKineticBladeName)
                .SetDescription(KDKineticBladeDescription)
                .SetIcon(AbilityRefs.BlessWeapon.Reference.Get().Icon)
                .SetIsClassFeature(true)
                .AddFacts(new List<Blueprint<BlueprintUnitFactReference>> { FeatureRefs.KineticBladeInfusion.Cast<BlueprintUnitFactReference>().Reference })
                .AddComponent(reduceBladeCost)
                .Configure();
        }

        private static BlueprintFeature KineticDualBladesFeature()
        {
            // Dual blade buff
            var increaseBladeCost = new AddKineticistBurnModifier
            {
                Value = 1,
                BurnType = KineticistBurnType.Infusion,
                m_AppliableTo = allBlades
            };

            var buff = BuffConfigurator.New(KineticDualBladesBuffName, KineticDualBladesBuffGuid)
                .SetDisplayName(KineticDualBladesBuffName)
                .SetDescription(KineticDualBladesBuffDescription)
                .SetIcon(AbilityRefs.DivineFavor.Reference.Get().Icon)
                .AddNotDispelable()
                .AddComponent(increaseBladeCost)
                .AddComponent(new ReactivateKineticBladeComponent())
                .Configure();

            // Dual blade ability
            var ability = ActivatableAbilityConfigurator.New(KineticDualBladesAbilityName, KineticDualBladesAbilityGuid)
                .SetDisplayName(KineticDualBladesAbilityName)
                .SetDescription(KineticDualBladesAbilityDescription)
                .SetIcon(AbilityRefs.DivineFavor.Reference.Get().Icon)
                .SetBuff(buff)
                .SetDeactivateImmediately()
                .SetDoNotTurnOffOnRest()
                .Configure();

            return FeatureConfigurator.New(KineticDualBladesFeatureName, KineticDualBladesFeatureGuid)
                .SetDisplayName(KineticDualBladesFeatureName)
                .SetDescription(KineticDualBladesFeatureDescription)
                .SetIcon(FeatureRefs.TwoWeaponFighting.Reference.Get().Icon)
                .SetIsClassFeature(true)
                .AddFacts(new List<Blueprint<BlueprintUnitFactReference>> {
                    FeatureRefs.TwoWeaponFighting.Cast<BlueprintUnitFactReference>().Reference,
                    ability.ToReference<BlueprintUnitFactReference>() })
                .Configure();
        }

        private static BlueprintFeature ImprovedKineticDualBladesFeature()
        {
            return FeatureConfigurator.New(ImprovedKineticDualBladesName, ImprovedKineticDualBladesGuid)
                .SetDisplayName(ImprovedKineticDualBladesName)
                .SetDescription(ImprovedKineticDualBladesDescription)
                .SetIcon(FeatureRefs.TwoWeaponFightingImproved.Reference.Get().Icon)
                .SetIsClassFeature(true)
                .Configure();
        }

        private static BlueprintFeature SynchronousChargeFeature()
        {
            return FeatureConfigurator.New(SynchronousChargeFeatureName, SynchronousChargeFeatureGuid)
                .SetDisplayName(SynchronousChargeFeatureName)
                .SetDescription(SynchronousChargeFeatureDescription)
                .SetIcon(AbilityRefs.JoyfulRapture.Reference.Get().Icon)
                .AddInitiatorAttackWithWeaponTrigger(
                    action: ActionsBuilder.New().Add(new Heal1BurnAcceptedThisTurn()),
                    triggerBeforeAttack: false,
                    onlyHit: true,
                    onlyOnFirstHit: true,
                    onlyOnFullAttack: true,
                    checkWeaponCategory: true,
                    category: WeaponCategory.KineticBlast)
                .SetIsClassFeature(true)
                .Configure();
        }

        private static BlueprintFeature KineticAssaultFeature()
        {
            // Restrict metakinesis quicken
            FeatureConfigurator.For(FeatureRefs.MetakinesisMasterQuicken)
                .AddPrerequisiteNoArchetype(ArchetypeGuid, CharacterClassRefs.KineticistClass.Reference.Get())
                .Configure();

            var abilityKineticistComponent = new AbilityKineticist
            {
                InfusionBurnCost = 3
            };

            var weaponCheckComponent = new AbilityCasterMainWeaponCheck
            {
                Category = new WeaponCategory[] { WeaponCategory.KineticBlast }
            };

            // Copy the restrictions of a charge
            var hasNoConditions = AbilityRefs.ChargeAbility.Reference.Get().GetComponent<AbilityRequirementHasCondition>();
            var hasNoFacts = AbilityRefs.ChargeAbility.Reference.Get().GetComponent<AbilityCasterHasNoFacts>();
            var canMove = AbilityRefs.ChargeAbility.Reference.Get().GetComponent<AbilityRequirementCanMove>();
            var fullRoundTB = new AbilityIsFullRoundInTurnBased { FullRoundIfTurnBased = true };

            // Auto-maximise and immune to AoO buff
            var buff = BuffConfigurator.New(KineticAssaultBuffName, KineticAssaultBuffGuid)
                .SetDisplayName(KineticAssaultBuffName)
                .SetDescription(KineticAssaultBuffDescription)
                .SetIcon(AbilityRefs.FreedomOfMovement.Reference.Get().Icon)
                .AddComponent(BuffRefs.MetakinesisMaximizedBuff.Reference.Get().GetComponent<AutoMetamagic>())
                .AddCondition(UnitCondition.ImmuneToAttackOfOpportunity)
                // Make an additional off hand attack if kinetic dual blades are active
                .AddInitiatorAttackWithWeaponTrigger(
                    action: ActionsBuilder.New().Add(new ExtraAttackWithOffhanBlade()),
                    triggerBeforeAttack: false,
                    onlyHit: false,
                    onlyOnFirstAttack: true,
                    checkWeaponCategory: true,
                    category: WeaponCategory.KineticBlast)
                .Configure();

            var contextActionApplyBuff = new ContextActionApplyBuff()
            {
                m_Buff = buff.ToReference<BlueprintBuffReference>(),
                ToCaster = true,
                DurationSeconds = 6,
                IsNotDispelable = true,
                UseDurationSeconds = true,
                IsFromSpell = false,
                AsChild = false,
            };

            var applyBuffAction = new AbilityExecuteActionOnCast()
            {
                Actions = new ActionList() { Actions = new GameAction[] { contextActionApplyBuff } },
                Conditions = new ConditionsChecker() { Operation = Operation.And, Conditions = Array.Empty<Condition>() }
            };
            
            var ability = AbilityConfigurator.New(KineticAssaultAbilityName, KineticAssaultAbilityGuid)
                .SetDisplayName(KineticAssaultAbilityName)
                .SetDescription(KineticAssaultAbilityDescription)
                .SetIcon(BuffRefs.FreedomOfMovementBuff.Reference.Get().Icon)
                .SetType(AbilityType.Special)
                .SetRange(AbilityRange.DoubleMove)
                .SetCanTargetEnemies()
                .SetShouldTurnToTarget()
                .SetEffectOnEnemy(AbilityEffectOnUnit.Harmful)
                .SetAnimation(Kingmaker.Visual.Animation.Kingmaker.Actions.UnitAnimationActionCastSpell.CastAnimationStyle.Immediate)
                .SetAvailableMetamagic(new Metamagic[] {Metamagic.Maximize, Metamagic.Quicken})
                .AddComponent(applyBuffAction)
                .AddComponent(new AbilityCustomCharge())
                .AddComponent(weaponCheckComponent)
                .AddComponent(hasNoConditions)
                .AddComponent(hasNoFacts)
                .AddComponent(fullRoundTB)
                .AddComponent(canMove)
                .AddComponent(abilityKineticistComponent)
                .Configure();
                
            return FeatureConfigurator.New(KineticAssaultFeatureName, KineticAssaultFeatureGuid)
                .SetDisplayName(KineticAssaultFeatureName)
                .SetDescription(KineticAssaultFeatureDescription)
                .SetIcon(BuffRefs.FreedomOfMovementBuff.Reference.Get().Icon)
                .SetIsClassFeature(true)
                .AddFacts(new List<Blueprint<BlueprintUnitFactReference>> { ability })
                .Configure();
        }

        private static BlueprintFeature GreaterKineticDualBladesFeature()
        {
            return FeatureConfigurator.New(GreaterKineticDualBladesName, GreaterKineticDualBladesGuid)
                .SetDisplayName(GreaterKineticDualBladesName)
                .SetDescription(GreaterKineticDualBladesDescription)
                .SetIcon(FeatureRefs.TwoWeaponFightingGreater.Reference.Get().Icon)
                .SetIsClassFeature(true)
                .Configure();
        }
    
        private static BlueprintBuff DualBlades2ndAttackBuff()
        {
            return BuffConfigurator.New(DualBlades2ndAttackBuffName, DualBlades2ndAttackBuffGuid)
                .SetDisplayName(DualBlades2ndAttackBuffName)
                .SetDescription(DualBlades2ndAttackBuffDescription)
                .SetIcon(FeatureRefs.TwoWeaponFightingImproved.Reference.Get().Icon)
                .SetFlags(new BlueprintBuff.Flags[] { BlueprintBuff.Flags.HiddenInUi })
                .AddComponent(new GiveExtraOffhandBladeAttack())
                .AddNotDispelable()
                .Configure();
        }

        private static BlueprintBuff DualBlades3rdAttackBuff()
        {
            return BuffConfigurator.New(DualBlades3rdAttackBuffName, DualBlades3rdAttackBuffGuid)
                .SetDisplayName(DualBlades3rdAttackBuffName)
                .SetDescription(DualBlades3rdAttackBuffDescription)
                .SetIcon(FeatureRefs.TwoWeaponFightingGreater.Reference.Get().Icon)
                .SetFlags(new BlueprintBuff.Flags[] { BlueprintBuff.Flags.HiddenInUi })
                .AddComponent(new GiveExtraOffhandBladeAttack())
                .AddNotDispelable()
                .Configure();
        }

        private static void RestrictFormInfusionSelections()
        {
            var formInfusions = new Blueprint<BlueprintReference<BlueprintFeature>>[]
            {
                FeatureRefs.CloudInfusion,
                FeatureRefs.EruptionInfusion,
                FeatureRefs.SprayInfusion,
                FeatureRefs.FragmentationInfusion,
                FeatureRefs.DeadlyEarthInfusion,
                FeatureRefs.CycloneInfusion,
                FeatureRefs.DetonationInfusion,
                FeatureRefs.WallInfusion,
                FeatureRefs.SpindleInfusion,
                FeatureRefs.TorrentInfusionFeature,
            };
            for (int i = 0; i < formInfusions.Length; i++) 
            {
                Blueprint<BlueprintReference<BlueprintFeature>> infusion = formInfusions[i];
                FeatureConfigurator.For(infusion)
                    .AddPrerequisiteNoArchetype(ArchetypeGuid, CharacterClassRefs.KineticistClass.Reference.Get())
                    .Configure();
            }
        }

        private static void RestrictModInfusionSelections()
        {
            var formInfusions = new List<BlueprintFeature>();
            var modInfusionGuids = new string[]
            {
                        "4b6884729a46432ea9b5e1a873e8efa6", // Chain infusion
                        "611f666629f7451c98618d62b16ed62e", // Impale infusion
                        "000706ddb53e468a926a3c36e1889213", // Force hook
                        "cba7fb8cef0c4160b500850d0c58d1d9", // Foe throw
                        "ae785f510e4c4ed2991b59b421c0a2e5", // Many throw
                        "2a8b8823924245aa9c9494679b311866", // Singularity
            };
            for (int i = 0; i < modInfusionGuids.Length; i++)
            {
                bool got = BlueprintTool.TryGet(modInfusionGuids[i], out BlueprintFeature bp);

                if (got)
                {
                    Logger.Info($"Mod infusion {bp} found");
                    formInfusions.Add(bp);
                }
                else
                    Logger.Info($"Mod infusion {modInfusionGuids[i]} not found");
            }
            for (int i = 0; i < formInfusions.Count; i++)
            {
                var infusion = formInfusions[i];
                FeatureConfigurator.For(infusion)
                    .AddPrerequisiteNoArchetype(ArchetypeGuid, CharacterClassRefs.KineticistClass.Reference.Get())
                    .Configure();
            }
        }

        private static void AddModBladeToFeatures()
        {
            var modBladeBurnGuids = new string[]
            {
                "5d81270056d24a2e88df79dfb983cbcd", // Telekinetic
                "0b8bc0ee998a41508052ca7ff31c14f8", // Force
                "4471efdc8c1440faba7110675ddb31af", // Negative
                "1ec348ed9dcb437eb601f20b98f25181", // Gravity
                "6b2bed26fcd847c1a571b5f2ea6cea0a", // Void
                "e7c2a4e7dcae40b09dda30a879123483", // Wood
                "22d5001563c74bdfa6e0c5602fd11eef", // Positive
                "2846063b5b6e4fea9bee612c1e24dd60", // Verdant
                "9e26ea79e09b4e1b9ee09cbd25ae1405", // Spring
                "b5c7051ae9c8450da9769c955971f0c2", // Summer
                "beb6066e7de74ff0a7eb9d0eb0f6ff36", // Autumn
                "380b9f337f2248dc82d4b1c7af8bd507", // Winter
            };
            var modBlades = new List<BlueprintAbilityReference>();
            for (int i = 0; i < modBladeBurnGuids.Length; i++) 
            {
                bool got = BlueprintTool.TryGet(modBladeBurnGuids[i], out BlueprintAbility bp);
                if (got)
                {
                    Logger.Info($"Mod blade {bp} found, adding refs to KineticDuelist");
                    modBlades.Add(bp.ToReference<BlueprintAbilityReference>());
                }
                else
                    Logger.Info($"Mod blade {modBladeBurnGuids[i]} not found");
            }
            if (modBlades.Count != 0)
                allBlades = allBlades.Concat(modBlades).ToArray();
            BlueprintTool.Get<BlueprintFeature>(KDKineticBladeGuid).GetComponent<AddKineticistBurnModifier>().m_AppliableTo = allBlades;
            BlueprintTool.Get<BlueprintBuff>(KineticDualBladesBuffGuid).GetComponent<AddKineticistBurnModifier>().m_AppliableTo = allBlades;
        }

        public static void HandleOtherMods()
        {
            RestrictModInfusionSelections();
            AddModBladeToFeatures();

            // Solution to make blade AoO compatible with DarkCodex's Patch_AllowAoO
            // If DarkCodex is enabled, this will make the save permenantly dependent on DarkCodex
            var CreateAddMechanicsFeature = AccessTools.Method("CodexLib.Helper, CodexLib:CreateAddMechanicsFeature", new[] { Type.GetType("CodexLib.MechanicFeature, CodexLib") });
            var comp = CreateAddMechanicsFeature?.Invoke(null, new object[] { 8 }) as BlueprintComponent;
            if (comp != null)
                FeatureConfigurator.For(KDKineticBladeGuid).AddComponent(comp).Configure();
        }
    }

    public class GiveExtraOffhandBladeAttack : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleCalculateAttacksCount>, IRulebookHandler<RuleCalculateAttacksCount>, ISubscriber, IInitiatorRulebookSubscriber
    {
        public void OnEventAboutToTrigger(RuleCalculateAttacksCount evt)
        {
            var offhand = Owner.Body.SecondaryHand;
            if (offhand.HasWeapon && offhand.Weapon.Blueprint.Type.Category == WeaponCategory.KineticBlast)
                evt.AddExtraAttacks(1, false, true, offhand.Weapon);
        }

        public void OnEventDidTrigger(RuleCalculateAttacksCount evt)
        {
        }
    }

    [TypeId("0ABAAC72-2ED7-4170-992A-6AEBFA5FD440")]
    public class Heal1BurnAcceptedThisTurn : ContextAction
    {
        public override string GetCaption()
        {
            return "Kineticist heal 1 burn accepted this turn";
        }

        public override void RunAction()
        {
            UnitPartKineticist unitPartKineticist = Context.MaybeCaster?.Get<UnitPartKineticist>();
            if (!unitPartKineticist)
            {
                KineticDuelist.Logger.Error("Caster is not kineticist");
            }
            else
            {
                if (unitPartKineticist.AcceptedBurnThisRound > 0)
                    unitPartKineticist.HealBurn(1);
            }
        }
    }

    [TypeId("E0240606-8CD2-4858-8510-4C328E342152")]
    public class ExtraAttackWithOffhanBlade : ContextAction
    {
        public override string GetCaption()
        {
            return "Kinetic duelist attacks with offhand blade";
        }

        public override void RunAction()
        {
            UnitPartKineticist unitPartKineticist = Context.MaybeCaster?.Get<UnitPartKineticist>();
            if (!unitPartKineticist)
            {
                KineticDuelist.Logger.Error("Caster is not kineticist");
            }
            else
            {
                var offhandBlade = Context.MaybeOwner?.Body.SecondaryHand.Weapon;
                if (offhandBlade != null && offhandBlade.Blueprint.Type.Category == WeaponCategory.KineticBlast)
                {
                    RuleAttackWithWeapon ruleAttackWithWeapon = new(
                        Context.MaybeCaster,
                        Target.Unit, 
                        offhandBlade, 0);
                    Context.TriggerRule(ruleAttackWithWeapon);
                }
            }
        }
    }

    [TypeId("4C86281E-8574-4A1C-8CAA-8098679E76B0")]
    public class ReactivateKineticBladeComponent : UnitFactComponentDelegate, IAreaActivationHandler, IGlobalSubscriber, ISubscriber
    {
        public void OnAreaActivated() { ReactivateKineticBlade(); }

        public override void OnActivate() { ReactivateKineticBlade(); }

        public override void OnDeactivate() { ReactivateKineticBlade(); }

        private void ReactivateKineticBlade()
        {
            var kineticist = Owner.Parts.Get<UnitPartKineticist>();
            BlueprintItemWeapon bladeBP = Owner?.Body.PrimaryHand.MaybeWeapon?.Blueprint;
            WeaponKineticBlade blade = bladeBP?.GetComponent<WeaponKineticBlade>();
            if (kineticist == null || blade == null)
                return;

            ActivatableAbility activeBlade = null;
            foreach (var activatable in Owner.Descriptor.ActivatableAbilities.RawFacts)
            {
                if (activatable.IsOn && activatable.Blueprint.Buff?.GetComponent<AddKineticistBlade>()?.Blade == bladeBP)
                {
                    activeBlade = activatable;
                    break;
                }
            }

            if (activeBlade == null)
                return;

            activeBlade.SetIsOn(value: false, null);
            activeBlade.Stop(forceRemovedBuff: true);

            if (activeBlade.IsAvailable)
                activeBlade.SetIsOn(true, null);
        }
    }

    [HarmonyPatch(typeof(AddKineticistBlade))]
    public class Patch_AddKineticistBlade
    {
        private static readonly LogWrapper Logger = LogWrapper.Get("KineticArchetypes.AddKineticistBlade");

        [HarmonyPostfix]
        [HarmonyPatch(nameof(AddKineticistBlade.OnActivate))]
        public static void Postfix1(AddKineticistBlade __instance)
        {
            UnitEntityData owner = __instance.Owner;
            if (owner is null)
                return;
            
            // Check for activatables
            bool dualbuff = false;
            bool spearbuff = false;
            bool vitalbuff = false;
            foreach (var buff in owner.Buffs)
            {
                if (buff.Blueprint.ToString().Equals(KineticDuelist.KineticDualBladesBuffName))
                    dualbuff = true;
                else if (buff.Blueprint.ToString().Equals(KineticLancer.KineticSpearBuffName))
                    spearbuff = true;
                else if (buff.Blueprint.ToString().Equals(KineticistGeneral.VitalBladeBuffName))
                    vitalbuff = true;
            }

            // Allow AoO if having KD blade feature or EsotericBlade feature
            if (owner.GetFeature(BlueprintTool.GetRef<BlueprintFeatureReference>(KineticDuelist.KDKineticBladeGuid)) != null ||
                owner.GetFeature(BlueprintTool.GetRef<BlueprintFeatureReference>(EsotericBlade.ConstantEnergyGuid)) != null || spearbuff)
                owner.State.RemoveCondition(UnitCondition.DisableAttacksOfOpportunity);

            // Spawn off-hand blade if dual blade activated
            if (dualbuff && !spearbuff)
            {
                var bladeOffHand = (ResourcesLibrary.TryGetBlueprint(__instance.m_Blade.Guid) as BlueprintItemWeapon).CreateEntity<ItemEntityWeapon>();
                bladeOffHand.MakeNotLootable();

                if (owner.Body.SecondaryHand.HasItem || !owner.Body.SecondaryHand.CanInsertItem(bladeOffHand))
                {
                    Logger.Info("Can't insert kineticist blade to off hand");
                }
                else
                {
                    using (ContextData<ItemsCollection.SuppressEvents>.Request())
                    {
                        owner.Body.SecondaryHand.InsertItem(bladeOffHand);
                    }
                    Logger.Info("Inserted kinetic blade to off hand");
                }

                // Add extra offhand attacks if having improved/greater dual blade features and no TWF features
                var TWFRank = owner.GetFeature(FeatureRefs.TwoWeaponFightingBasicMechanics.Reference.Get())?.GetRank();
                if (owner.GetFeature(BlueprintTool.GetRef<BlueprintFeatureReference>(KineticDuelist.ImprovedKineticDualBladesGuid)) != null && TWFRank != null && TWFRank < 3)
                    owner.AddBuff(BlueprintTool.GetRef<BlueprintBuffReference>(KineticDuelist.DualBlades2ndAttackBuffGuid), owner);
                if (owner.GetFeature(BlueprintTool.GetRef<BlueprintFeatureReference>(KineticDuelist.GreaterKineticDualBladesGuid)) != null && TWFRank != null && TWFRank < 4)
                    owner.AddBuff(BlueprintTool.GetRef<BlueprintBuffReference>(KineticDuelist.DualBlades3rdAttackBuffGuid), owner);
            }
            else if (spearbuff)
            {
                if (owner.Body.SecondaryHand.HasItem)
                    owner.Body.SecondaryHand.RemoveItem();
                owner.Body.SecondaryHand.Lock.Retain();
                owner.AddBuff(BlueprintTool.Get<BlueprintBuff>(KineticLancer.KineticSpearRealBuffGuid), owner);
            }

            if (vitalbuff)
            {
                owner.AddBuff(BlueprintTool.Get<BlueprintBuff>(KineticistGeneral.VitalBladeRealBuffGuid), owner);
            }

            var rememberedWeapon = owner.Parts.Ensure<RememberWeaponPart>().RememberedWeapon;
            if (spearbuff)
                rememberedWeapon = owner.Parts.Get<RememberWeaponPart>().RememberedLongSpear ??
                                        ItemWeaponRefs.ColdIronLongspear.Reference.Get();
            foreach (var hand in new HandSlot[] {
                owner.Body.CurrentHandsEquipmentSet.PrimaryHand, owner.Body.CurrentHandsEquipmentSet.SecondaryHand })
                if (hand.Weapon?.Blueprint.Type.Category == WeaponCategory.KineticBlast && rememberedWeapon != null)
                {
                    Main.Logger.Info($"Overriding visual for kinetic blade of {hand.IsPrimaryHand} into {rememberedWeapon}");

                    hand.Weapon.VisualSourceItemBlueprint = rememberedWeapon;
                    owner.View.HandsEquipment.UpdateActiveWeaponSetImmediately();
                }
            // Fix kinetic spear not on back
            if (rememberedWeapon != null && rememberedWeapon.Type.Category == WeaponCategory.Longspear)
            {
                owner.View.HandsEquipment.m_ActiveSet.MainHand.VisualSlot = 0;
                owner.View.HandsEquipment.FindSlotForHand(owner.View.HandsEquipment.m_ActiveSet.MainHand, new List<UnitEquipmentVisualSlotType>(), force: false);
                owner.View.HandsEquipment.ReattachBackEquipment();
            }
        }


        [HarmonyPrefix]
        [HarmonyPatch(nameof(AddKineticistBlade.OnDeactivate))]
        public static void Prefix2(AddKineticistBlade __instance)
        {
            var owner = __instance.Owner;

            var handsSets = owner.Body.HandsEquipmentSets;
            foreach (var handsSet in handsSets)
            {
                var mainhand = handsSet.PrimaryHand;
                var offhand = handsSet.SecondaryHand;
                if (offhand != null && offhand.HasWeapon && offhand.HasItem)
                {
                    // This variable is ESSENTIAL!!!
                    var weapon = offhand.Item as ItemEntityWeapon;
                    if (weapon.Blueprint.Type.Category == WeaponCategory.KineticBlast)
                    {
                        Logger.Info("Removing kinetic blade from off hand");
                        weapon.HoldingSlot.Lock.ReleaseAll();
                        weapon.HoldingSlot?.RemoveItem();
                        using (ContextData<ItemsCollection.SuppressEvents>.Request())
                            weapon.Collection?.Remove(weapon);

                        Logger.Info($"Dispose garbage entity {weapon}");
                        weapon.Dispose();
                    }
                }
                if (owner.GetFeature(BlueprintTool.Get<BlueprintFeature>(KineticLancer.KineticSpearGuid)) != null &&
                    mainhand != null && mainhand.HasWeapon && mainhand.HasItem &&
                    (mainhand.Item as ItemEntityWeapon).Blueprint.Type.Category == WeaponCategory.KineticBlast)
                    offhand.Lock.ReleaseAll();
            }

            // Remove extra attack buffs and spear buff
            foreach (var buff in owner.Buffs)
            {
                if (buff.Blueprint.ToString().Equals(KineticDuelist.DualBlades2ndAttackBuffName) || 
                    buff.Blueprint.ToString().Equals(KineticDuelist.DualBlades3rdAttackBuffName) ||
                    buff.Blueprint.ToString().Equals(KineticLancer.KineticSpearRealBuffName)     ||
                    buff.Blueprint.ToString().Equals(KineticistGeneral.VitalBladeRealBuffName))
                {
                    buff.SetDuration(TimeSpan.FromSeconds(0));
                }
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(AddKineticistBlade.OnTurnOn))]
        public static void Postfix3(AddKineticistBlade __instance)
        {
            var handsSets = __instance.Owner.Body.HandsEquipmentSets;
            foreach (var handsSet in handsSets)
            {
                var offhand = handsSet.SecondaryHand;
                if (offhand.HasWeapon)
                {
                    if (offhand.Weapon.Blueprint.Type.Category == WeaponCategory.KineticBlast)
                    {
                        offhand.Weapon.HoldingSlot.Lock.Retain();
                    }
                }
            }
        }
    }

    [HarmonyPatch(typeof(RestrictionCanUseKineticBlade))]
    public class Patch_CanUseKineticBlade
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(RestrictionCanUseKineticBlade.IsAvailable), new Type[] { typeof(UnitDescriptor) })]
        public static bool Prefix1(RestrictionCanUseKineticBlade __instance, ref bool __result, UnitDescriptor unit)
        {
            UnitBody body = unit.Body;
            if ((body.IsPolymorphed && !body.IsPolymorphKeepSlots) || !body.HandsAreEnabled)
            {
                __result = false;
                return false;
            }
            UnitPartKineticist unitPartKineticist = unit.Get<UnitPartKineticist>();
            if (!unitPartKineticist)
            {
                __result = false;
                return false;
            }
            BlueprintItemWeapon blueprintItemWeapon = body.PrimaryHand.MaybeWeapon?.Blueprint;
            bool flag = blueprintItemWeapon.GetComponent<WeaponKineticBlade>() != null;
            if (body.PrimaryHand.MaybeItem != null && !flag)
            {
                __result = false;
                return false;
            }
            BlueprintItemWeapon blueprintItemWeapon2 = BlueprintComponentExtendAsObject.Or(__instance.Fact.Blueprint.Buff.GetComponent<AddKineticistBlade>(), null)?.Blade;
            if (blueprintItemWeapon2 == null)
            {
                __result = false;
                return false;
            }

            // Trace back the burn at the start of round by adding this back to left burn this round
            // Will create situations of having used burn for other stuff but still passing check for blade
            var acceptedBurnThisRound = unitPartKineticist.AcceptedBurnThisRound;
            int cost = AbilityKineticist.CalculateAbilityBurnCost(BlueprintComponentExtendAsObject.Or(blueprintItemWeapon2.GetComponent<WeaponKineticBlade>(), null)?.GetActivationAbility(unit))?.Total ?? 0;
            if (unit.GetFact(BlueprintTool.Get<BlueprintFeature>(KineticLancer.DragoonDiveGuid)) != null)
            {
                cost = DragoonDiveBurnDisplay.CalculateCostForBlade(unit, blueprintItemWeapon2.GetComponent<WeaponKineticBlade>());
            }

            if ((blueprintItemWeapon != blueprintItemWeapon2 || !unitPartKineticist.IsBladeActivated) && cost > unitPartKineticist.LeftBurnThisRound + acceptedBurnThisRound)
            {
                __result = false;
                return false;
            }
            __result = true;
            return false;
        }
    }
}
