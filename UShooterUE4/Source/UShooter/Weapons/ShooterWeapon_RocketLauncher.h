// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "ShooterWeaponComponent.h"
#include "ShooterWeapon_RocketLauncher.generated.h"


UCLASS( ClassGroup=(Custom), meta=(BlueprintSpawnableComponent), hidecategories = (Tags, Activation, Variable, ComponentTick, Collision))
class USHOOTER_API UShooterWeapon_RocketLauncher: public UShooterWeaponComponent
{
	GENERATED_BODY()

public:	
	// Sets default values for this component's properties
	UShooterWeapon_RocketLauncher();
	
	// Called every frame
	virtual void TickComponent( float DeltaTime, ELevelTick TickType, FActorComponentTickFunction* ThisTickFunction ) override;

	// Fires projectile
	void Fire() override;

	// Rocket type
	UPROPERTY(EditDefaultsOnly, BlueprintReadOnly, Category = "Rocket")
	TSubclassOf<class ARocketProjectile> RocketType;
};
