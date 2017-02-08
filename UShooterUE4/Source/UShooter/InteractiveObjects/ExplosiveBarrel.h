// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "DestructibleObject.h"
#include "ExplosiveBarrel.generated.h"

UCLASS()
class USHOOTER_API AExplosiveBarrel : public ADestructibleObject
{
	GENERATED_BODY()
	
public:	
	// Sets default values for this actor's properties
	AExplosiveBarrel();

	// Destruction handling
	virtual void OnDestroyed() override;
};
