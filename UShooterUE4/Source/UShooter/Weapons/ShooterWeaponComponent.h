// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "Components/ActorComponent.h"
#include "ShooterWeaponComponent.generated.h"

UENUM(BlueprintType)
enum class EWeaponType : uint8
{
	Default = 0,		// Unknown weapon
	RocketLauncher = 1,
	RailGun = 2
};

UCLASS( ClassGroup=(Custom), meta=(BlueprintSpawnableComponent), hidecategories = (Tags, Activation, Variable, ComponentTick, Collision))
class USHOOTER_API UShooterWeaponComponent: public UActorComponent
{
	GENERATED_BODY()

public:	
	// Sets default values for this component's properties
	UShooterWeaponComponent();
	
	// Called every frame
	virtual void TickComponent( float DeltaTime, ELevelTick TickType, FActorComponentTickFunction* ThisTickFunction ) override;

	// Fires weapon
	virtual void Fire();

	// Weapon Damage
	UPROPERTY(EditDefaultsOnly, BlueprintReadOnly, Category = "Weapon")
	float WeaponDamage;

	// Weapon type
	UPROPERTY(EditDefaultsOnly, BlueprintReadOnly, Category = "Weapon")
	EWeaponType WeaponType;

	// Current ammo count
	UPROPERTY(EditDefaultsOnly, BlueprintReadOnly, Category = "Weapon")
	int32 CurrentAmmoCount;

	// Firing effect
	UPROPERTY(EditDefaultsOnly, BlueprintReadOnly, Category = "Weapon")
	class UParticleSystem* MuzzleEffect;
};
