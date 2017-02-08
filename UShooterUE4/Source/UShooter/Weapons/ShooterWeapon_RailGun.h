// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "ShooterWeaponComponent.h"
#include "ShooterWeapon_RailGun.generated.h"


UCLASS( ClassGroup=(Custom), meta=(BlueprintSpawnableComponent), hidecategories = (Tags, Activation, Variable, ComponentTick, Collision))
class USHOOTER_API UShooterWeapon_RailGun: public UShooterWeaponComponent
{
	GENERATED_BODY()

public:	
	// Sets default values for this component's properties
	UShooterWeapon_RailGun();
	
	// Called every frame
	virtual void TickComponent( float DeltaTime, ELevelTick TickType, FActorComponentTickFunction* ThisTickFunction ) override;

	// Fires projectile
	void Fire() override;

	// Rail effect
	UPROPERTY(EditDefaultsOnly, BlueprintReadOnly, Category = "Rail Gun")
	class UParticleSystem* RailTrailEffect;
};
