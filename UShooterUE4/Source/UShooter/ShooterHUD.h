// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "GameFramework/HUD.h"
#include "ShooterHUD.generated.h"

/**
 * 
 */
UCLASS()
class USHOOTER_API AShooterHUD : public AHUD
{
	GENERATED_BODY()

public:

	/** Player has changed weapon */
	UFUNCTION(BlueprintImplementableEvent)
	void OnWeaponChanged(class UShooterWeaponComponent* Weapon);

	/** Player has fired weapon */
	UFUNCTION(BlueprintImplementableEvent)
	void OnWeaponFired(class UShooterWeaponComponent* Weapon);
};
