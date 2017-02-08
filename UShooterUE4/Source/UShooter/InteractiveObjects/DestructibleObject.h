// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "GameFramework/Actor.h"
#include "DestructibleObject.generated.h"

UCLASS()
class USHOOTER_API ADestructibleObject : public AActor
{
	GENERATED_BODY()
	
public:	
	// Sets default values for this actor's properties
	ADestructibleObject();

	// Called when the game starts or when spawned
	virtual void BeginPlay() override;

	/** Damage handler */
	virtual float TakeDamage(float DamageAmount, struct FDamageEvent const& DamageEvent, class AController* EventInstigator, class AActor* DamageCauser) override;

	// Destruction effect
	UPROPERTY(EditDefaultsOnly, BlueprintReadOnly, Category = "Destructible Object")
	class UParticleSystem* DestructionEffect;

	// Health
	UPROPERTY(EditDefaultsOnly, BlueprintReadOnly, Category = "Destructible Object")
	float Health;

	// Destruction handling
	virtual void OnDestroyed();

private:

	/** Collision sphere */
	UPROPERTY(VisibleDefaultsOnly, Category = "Destructible Object")
	USphereComponent* CollisionComp;
};
