using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public PlayerData playerData;

	[Header("Audio Sources")]
	public AudioSource foot;

	public AudioSource weapon;

	public AudioSource UI;

	public AudioSource player;

	public AudioSource ambient;

	public AudioSource thunder;

	public AudioSource wind;

	public AudioSource storm;

	public AudioSource flyby;

	public AudioSource casing;

	public AudioClip takeDamageDemo;

	public AmbientEvent breathing;

	[Header("Ambient Audio")]
	public AmbientEvent dawnNeutral;

	public AmbientEvent dayNeutral;

	public AmbientEvent duskNeutral;

	public AmbientEvent nightNeutral;

	public AmbientEvent rainAmbient;

	public AmbientEvent stormAmbient;

	public AmbientEvent windAmbient;

	public AudioEvent lightningStrike;

	[Header("Weapons / Handling")]
	public AudioEvent rifleFire;

	public AudioEvent SA85Fire;

	public AudioEvent g19Fire;

	public AudioEvent RM870Fire;

	public AudioEvent handling;

	[Header("Footsteps")]
	public AudioEvent footstepGrass;

	public AudioEvent footstepGrassJump;

	public AudioEvent footstepGrassLand;

	public AudioEvent footstepDirt;

	public AudioEvent footstepDirtJump;

	public AudioEvent footstepDirtLand;

	public AudioEvent footstepAsphalt;

	public AudioEvent footstepAsphaltJump;

	public AudioEvent footstepAsphaltLand;

	public AudioEvent footstepExtra;

	public AudioEvent footstepExtraJump;

	public AudioEvent footstepExtraLand;

	public AudioEvent footstepConcrete;

	public AudioEvent footstepConcreteJump;

	public AudioEvent footstepConcreteLand;

	public AudioEvent footstepMetal;

	public AudioEvent footstepMetalJump;

	public AudioEvent footstepMetalLand;

	public AudioEvent footstepRock;

	public AudioEvent footstepRockJump;

	public AudioEvent footstepRockLand;

	public AudioEvent footstepWood;

	public AudioEvent footstepWoodJump;

	public AudioEvent footstepWoodLand;

	[Header("Interaction")]
	public AudioEvent click;

	public AudioEvent UIToggle;

	public AudioEvent error;

	public AudioEvent pickup;

	public AudioEvent generateLoot;

	public AudioEvent candleLight;

	[Header("Medical")]
	public AudioEvent applyBandage;

	[Header("Debug")]
	public AudioEvent equipmentRattle;

	public AudioEvent hitTarget;

	public AudioEvent jumpGrunt;

	public AudioEvent doorOpen;

	public AudioEvent doorClose;

	public AudioEvent openZipper;

	public AudioEvent transition;

	[Header("Casing & Shell Drop")]
	public AudioEvent casingDropSoft;

	public AudioEvent casingDropHard;

	public AudioEvent casingDropWood;

	public AudioEvent shellDropSoft;

	public AudioEvent shellDropHard;

	public AudioEvent shellDropWood;

	[Space]
	public AudioSource audioSource;

	public AudioEvent rifleCock;

	public AudioEvent rifleUnloadMag;

	public AudioEvent rifleLoadMag;

	public AudioEvent rifleEquip;

	public AudioEvent rifleUnequip;

	public AudioEvent holdBreath;

	public AudioEvent switchFiremode;

	public AudioEvent inspectWeapon;

	public AudioEvent rifleEmpty;

	public AudioEvent rifleHandling;

	public AudioEvent laserToggle;

	public AudioEvent takeDamage;

	public AudioEvent impactBody;

	public AudioEvent bulletFlyby;

	public AudioEvent deathFall;

	public void PlayCasingDropHard()
	{
		casingDropHard.Play(casing, 1f);
	}

	public void PlayCasingDropSoft()
	{
		casingDropSoft.Play(casing, 1f);
	}

	public void PlayCasingDropWood()
	{
		casingDropWood.Play(casing, 1f);
	}

	public void PlayShellDropHard()
	{
		shellDropHard.Play(casing, 1f);
	}

	public void PlayShellDropSoft()
	{
		shellDropSoft.Play(casing, 1f);
	}

	public void PlayShellDropWood()
	{
		shellDropWood.Play(casing, 1f);
	}

	public void PlayBreathing()
	{
		breathing.Play(player, 1f);
	}

	public void PlayCandleLight()
	{
		candleLight.Play(player, 1f);
	}

	public void UseMedkit()
	{
		openZipper.Play(player, 1f);
		applyBandage.Play(player, 1f);
	}

	public void PlayRM870Fire()
	{
		RM870Fire.Play(weapon, 1f);
	}

	public void PlaySA85Fire()
	{
		SA85Fire.Play(weapon, 1f);
	}

	public void PlayDawn()
	{
		ambient.Stop();
		wind.Stop();
		storm.Stop();
		thunder.Stop();
		dawnNeutral.Play(ambient, 1f);
	}

	public void PlayDay()
	{
		ambient.Stop();
		wind.Stop();
		storm.Stop();
		thunder.Stop();
		dayNeutral.Play(ambient, 1f);
	}

	public void PlayDusk()
	{
		ambient.Stop();
		wind.Stop();
		storm.Stop();
		thunder.Stop();
		duskNeutral.Play(ambient, 1f);
	}

	public void PlayNight()
	{
		ambient.Stop();
		wind.Stop();
		storm.Stop();
		thunder.Stop();
		nightNeutral.Play(ambient, 1f);
	}

	public void PlayRain()
	{
		ambient.Stop();
		wind.Stop();
		storm.Stop();
		thunder.Stop();
		rainAmbient.Play(ambient, 1f);
	}

	public void PlayStorm()
	{
		ambient.Stop();
		wind.Stop();
		storm.Stop();
		thunder.Stop();
		rainAmbient.Play(ambient, 1f);
		stormAmbient.Play(storm, 1f);
		windAmbient.Play(wind, 0.5f);
	}

	public void PlayThunder()
	{
		lightningStrike.Play(thunder, 1f);
	}

	public void PlayEquipmentRattle()
	{
		equipmentRattle.Play(player, 1f);
	}

	public void PlayJumpGrunt()
	{
		jumpGrunt.Play(player, 1f);
	}

	public void PlayFootstepGrass()
	{
		if (playerData.isCrouching)
		{
			footstepGrass.Play(foot, 0.5f);
		}
		if (playerData.isWalking)
		{
			footstepGrass.Play(foot, 1f);
		}
		if (playerData.isRunning)
		{
			footstepGrass.Play(foot, 1.5f);
		}
		if (playerData.isSprinting)
		{
			footstepGrass.Play(foot, 2f);
		}
	}

	public void PlayFootstepGrassJump()
	{
		footstepGrassJump.Play(foot, 1f);
	}

	public void PlayFootstepGrassLand()
	{
		footstepGrassLand.Play(foot, 1f);
	}

	public void PlayFootstepDirt()
	{
		if (playerData.isCrouching)
		{
			footstepDirt.Play(foot, 0.5f);
		}
		if (playerData.isWalking)
		{
			footstepDirt.Play(foot, 1f);
		}
		if (playerData.isRunning)
		{
			footstepDirt.Play(foot, 1.5f);
		}
		if (playerData.isSprinting)
		{
			footstepDirt.Play(foot, 2f);
		}
	}

	public void PlayFootstepDirtJump()
	{
		footstepDirtJump.Play(foot, 1f);
	}

	public void PlayFootstepDirtLand()
	{
		footstepDirtLand.Play(foot, 1f);
	}

	public void PlayFootstepAsphalt()
	{
		if (playerData.isCrouching)
		{
			footstepAsphalt.Play(foot, 0.5f);
		}
		if (playerData.isWalking)
		{
			footstepAsphalt.Play(foot, 1f);
		}
		if (playerData.isRunning)
		{
			footstepAsphalt.Play(foot, 1.5f);
		}
		if (playerData.isSprinting)
		{
			footstepAsphalt.Play(foot, 2f);
		}
	}

	public void PlayFootstepAsphaltJump()
	{
		footstepAsphaltJump.Play(foot, 1f);
	}

	public void PlayFootstepAsphaltLand()
	{
		footstepAsphaltLand.Play(foot, 1f);
	}

	public void PlayFootstepExtra()
	{
		if (playerData.isCrouching)
		{
			footstepExtra.Play(foot, 0.5f);
		}
		if (playerData.isWalking)
		{
			footstepExtra.Play(foot, 1f);
		}
		if (playerData.isRunning)
		{
			footstepExtra.Play(foot, 1.5f);
		}
		if (playerData.isSprinting)
		{
			footstepExtra.Play(foot, 2f);
		}
	}

	public void PlayFootstepExtraJump()
	{
		footstepExtraJump.Play(foot, 1f);
	}

	public void PlayFootstepExtraLand()
	{
		footstepExtraLand.Play(foot, 1f);
	}

	public void PlayFootstepConcrete()
	{
		if (playerData.isCrouching)
		{
			footstepConcrete.Play(foot, 0.5f);
		}
		if (playerData.isWalking)
		{
			footstepConcrete.Play(foot, 1f);
		}
		if (playerData.isRunning)
		{
			footstepConcrete.Play(foot, 1.5f);
		}
		if (playerData.isSprinting)
		{
			footstepConcrete.Play(foot, 2f);
		}
	}

	public void PlayFootstepConcreteJump()
	{
		footstepConcreteJump.Play(foot, 1f);
	}

	public void PlayFootstepConcreteLand()
	{
		footstepConcreteLand.Play(foot, 1f);
	}

	public void PlayFootstepMetal()
	{
		if (playerData.isCrouching)
		{
			footstepMetal.Play(foot, 0.5f);
		}
		if (playerData.isWalking)
		{
			footstepMetal.Play(foot, 1f);
		}
		if (playerData.isRunning)
		{
			footstepMetal.Play(foot, 1.5f);
		}
		if (playerData.isSprinting)
		{
			footstepMetal.Play(foot, 2f);
		}
	}

	public void PlayFootstepMetalJump()
	{
		footstepMetalJump.Play(foot, 1f);
	}

	public void PlayFootstepMetalLand()
	{
		footstepMetalLand.Play(foot, 1f);
	}

	public void PlayFootstepRock()
	{
		if (playerData.isCrouching)
		{
			footstepRock.Play(foot, 0.5f);
		}
		if (playerData.isWalking)
		{
			footstepRock.Play(foot, 1f);
		}
		if (playerData.isRunning)
		{
			footstepRock.Play(foot, 1.5f);
		}
		if (playerData.isSprinting)
		{
			footstepRock.Play(foot, 2f);
		}
	}

	public void PlayFootstepRockJump()
	{
		footstepRockJump.Play(foot, 1f);
	}

	public void PlayFootstepRockLand()
	{
		footstepRockLand.Play(foot, 1f);
	}

	public void PlayFootstepWood()
	{
		if (playerData.isCrouching)
		{
			footstepWood.Play(foot, 0.5f);
		}
		if (playerData.isWalking)
		{
			footstepWood.Play(foot, 1f);
		}
		if (playerData.isRunning)
		{
			footstepWood.Play(foot, 1.5f);
		}
		if (playerData.isSprinting)
		{
			footstepWood.Play(foot, 2f);
		}
	}

	public void PlayFootstepWoodJump()
	{
		footstepWoodLand.Play(foot, 1f);
	}

	public void PlayFootstepWoodLand()
	{
		footstepWoodLand.Play(foot, 1f);
	}

	public void PlayError()
	{
		error.Play(UI, 1f);
	}

	public void PlayBulletFlyby()
	{
		if (Random.Range(0, 2) == 1)
		{
			float panStereo = Random.Range(-1f, 2f);
			flyby.panStereo = panStereo;
			bulletFlyby.Play(flyby, 1f);
		}
	}

	public void PlayG19Fire()
	{
		g19Fire.Play(weapon, 1f);
	}

	public void PlayUIToggle()
	{
		UIToggle.Play(UI, 1f);
	}

	public void PlayHandling()
	{
		handling.Play(player, 1f);
	}

	public void PlayDoorOpen(AudioSource audioSource)
	{
		doorOpen.Play(audioSource, 1f);
	}

	public void PlayDoorClose(AudioSource audioSource)
	{
		doorClose.Play(audioSource, 1f);
	}

	public void PlayDeathFall()
	{
		deathFall.Play(player, 1f);
	}

	public void PlayGenerateLoot()
	{
		generateLoot.Play(UI, 1f);
	}

	public void PlayOpenContainer(AudioEvent container)
	{
		container.Play(player, 1f);
	}

	public void PlayPickup()
	{
		pickup.Play(player, 1f);
	}

	public void PlayHitTarget()
	{
		hitTarget.Play(player, 1f);
	}

	public void PlayClick()
	{
		click.Play(UI, 1f);
	}

	public void PlayRifleEquip()
	{
		rifleEquip.Play(weapon, 1f);
	}

	public void PlayRifleUnequip()
	{
		rifleUnequip.Play(weapon, 1f);
	}

	public void PlayRifleCock()
	{
		rifleCock.Play(weapon, 1f);
	}

	public void PlayRifleUnloadMag()
	{
		rifleUnloadMag.Play(weapon, 1f);
	}

	public void PlayRifleLoadMag()
	{
		rifleLoadMag.Play(weapon, 1f);
	}

	public void PlayHoldBreath()
	{
		holdBreath.Play(player, 1f);
	}

	public void PlaySwitchFiremode()
	{
		switchFiremode.Play(weapon, 1f);
	}

	public void PlayInspectWeapon()
	{
		inspectWeapon.Play(weapon, 1f);
	}

	public void PlayRifleFire()
	{
		rifleFire.Play(weapon, 1f);
	}

	public void PlayRifleEmpty()
	{
		rifleEmpty.Play(weapon, 1f);
	}

	public void PlayRifleHandling()
	{
		rifleHandling.Play(weapon, 1f);
	}

	public void PlayLaserToggle()
	{
		laserToggle.Play(player, 1f);
	}

	public void PlayFlashlightToggle()
	{
		laserToggle.Play(player, 1f);
	}

	public void PlayTakeDamage()
	{
		takeDamage.Play(player, 1f);
	}

	public void PlayImpactBody()
	{
		impactBody.Play(player, 1f);
	}

	public void PlayWeaponFire(AudioEvent fire)
	{
		fire.Play(weapon, 1f);
	}

	public void PlayWeaponSound(AudioEvent audioEvent)
	{
		audioEvent.Play(weapon, 1f);
	}

	public void PlayUseSound(AudioEvent audioEvent)
	{
		audioEvent.Play(player, 1f);
	}

	public void PlayTransition()
	{
		transition.Play(player, 1f);
	}
}
