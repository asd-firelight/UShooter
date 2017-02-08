// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "GameFramework/Actor.h"
#include "RocketProjectile.generated.h"

UCLASS()
class USHOOTER_API ARocketProjectile : public AActor
{
	GENERATED_BODY()
	
public:	
	// Sets default values for this actor's properties
	ARocketProjectile();

	// Called when the game starts or when spawned
	virtual void BeginPlay() override;
	
	// Called every frame
	virtual void Tick( float DeltaSeconds ) override;

	// Rocket fly speed
	UPROPERTY(EditDefaultsOnly, BlueprintReadOnly, Category = "Rocket")
	float FlySpeed;

	// Rocket damage
	UPROPERTY(EditDefaultsOnly, BlueprintReadOnly, Category = "Rocket")
	float Damage;

	/** Initial setup, after components initialize */
	virtual void PostInitializeComponents() override;

	// Impact (collsion) handling
	UFUNCTION()
	void OnImpact(UPrimitiveComponent* HitComponent, AActor* OtherActor, UPrimitiveComponent* OtherComp, FVector NormalImpulse, const FHitResult& Hit);

	// Explosion effect
	UPROPERTY(EditDefaultsOnly, BlueprintReadOnly, Category = "Rocket")
	class UParticleSystem* ExplosionEffect;

private:

	/** Collision sphere */
	UPROPERTY(VisibleDefaultsOnly, Category = "Projectile")
	USphereComponent* CollisionComp;
};
