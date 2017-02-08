// Fill out your copyright notice in the Description page of Project Settings.

#include "UShooter.h"
#include "RocketProjectile.h"


// Sets default values
ARocketProjectile::ARocketProjectile()
{
	// Set this actor to call Tick() every frame.  You can turn this off to improve performance if you don't need it.
	PrimaryActorTick.bCanEverTick = true;

	CollisionComp = CreateDefaultSubobject<USphereComponent>(TEXT("SphereComp"));

	CollisionComp->InitSphereRadius(50.0f);
	CollisionComp->bTraceComplexOnMove = true;

	CollisionComp->BodyInstance.bNotifyRigidBodyCollision = true;
	CollisionComp->BodyInstance.bUseCCD = true;
	CollisionComp->OnComponentHit.AddDynamic(this, &ARocketProjectile::OnImpact);
	RootComponent = CollisionComp;

	FlySpeed = 500.0f;
	Damage = 50.f;
	ExplosionEffect = nullptr;
}

void ARocketProjectile::PostInitializeComponents()
{
	Super::PostInitializeComponents();

	if (Instigator != nullptr)
	{
		CollisionComp->MoveIgnoreActors.Add(Instigator);
	}
}

// Called when the game starts or when spawned
void ARocketProjectile::BeginPlay()
{
	Super::BeginPlay();
	
}

// Called every frame
void ARocketProjectile::Tick( float DeltaTime )
{
	Super::Tick( DeltaTime );

	FVector MoveDelta = GetActorForwardVector() * DeltaTime * FlySpeed;

	FHitResult Hit;
	RootComponent->MoveComponent(MoveDelta, GetActorRotation(), true, &Hit, MOVECOMP_NoFlags);
	if (Hit.bBlockingHit)
	{
		OnImpact(Hit.GetComponent(), Hit.GetActor(), Hit.GetComponent(), GetActorForwardVector(), Hit);
	}
}

void ARocketProjectile::OnImpact(UPrimitiveComponent* HitComponent, AActor* OtherActor, UPrimitiveComponent* OtherComp, FVector NormalImpulse, const FHitResult& Hit)
{
	if (OtherActor)
	{
		FDamageEvent DmgEvent;
		OtherActor->TakeDamage(Damage, DmgEvent, nullptr, this);
	}

	if (ExplosionEffect != nullptr)
	{
		UGameplayStatics::SpawnEmitterAtLocation(GetWorld(), ExplosionEffect, GetActorLocation());		
	}
	GetWorld()->DestroyActor(this);
}

