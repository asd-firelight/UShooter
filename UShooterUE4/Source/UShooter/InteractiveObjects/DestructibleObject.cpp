// Fill out your copyright notice in the Description page of Project Settings.

#include "UShooter.h"
#include "DestructibleObject.h"


// Sets default values
ADestructibleObject::ADestructibleObject()
{
	// Set this actor to call Tick() every frame.  You can turn this off to improve performance if you don't need it.
	PrimaryActorTick.bCanEverTick = false;

	CollisionComp = CreateDefaultSubobject<USphereComponent>(TEXT("SphereComp"));

	CollisionComp->InitSphereRadius(50.0f);
	RootComponent = CollisionComp;

	DestructionEffect = nullptr;
	Health = 20.0f;
}

void ADestructibleObject::OnDestroyed()
{
	if (DestructionEffect != nullptr)
	{
		UGameplayStatics::SpawnEmitterAtLocation(GetWorld(), DestructionEffect, GetActorLocation());
	}
	GetWorld()->DestroyActor(this);
}

float ADestructibleObject::TakeDamage(float DamageAmount, struct FDamageEvent const& DamageEvent, class AController* EventInstigator, class AActor* DamageCauser)
{
	if (Health > 0)
	{
		Health -= DamageAmount;
		if (Health < 0.f || FMath::IsNearlyZero(Health))
		{
			OnDestroyed();
		}
	}

	return DamageAmount;
}

// Called when the game starts or when spawned
void ADestructibleObject::BeginPlay()
{
	Super::BeginPlay();
	
}
