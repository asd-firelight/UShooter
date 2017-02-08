// Fill out your copyright notice in the Description page of Project Settings.

#include "UShooter.h"
#include "ShooterWeapon_RocketLauncher.h"
#include "RocketProjectile.h"
#include "UShooterCharacter.h"

UShooterWeapon_RocketLauncher::UShooterWeapon_RocketLauncher()
{
	RocketType = nullptr;

	WeaponType = EWeaponType::RocketLauncher;
}

void UShooterWeapon_RocketLauncher::TickComponent( float DeltaTime, ELevelTick TickType, FActorComponentTickFunction* ThisTickFunction )
{
	Super::TickComponent( DeltaTime, TickType, ThisTickFunction );

}

void UShooterWeapon_RocketLauncher::Fire()
{
	if (CurrentAmmoCount <= 0)
	{
		return;
	}

	AUShooterCharacter* OwnerCharacter = Cast<AUShooterCharacter>(GetOwner());

	if (OwnerCharacter != nullptr)
	{
		Super::Fire();

		FVector SpawnLocation = OwnerCharacter->GetMesh()->GetSocketLocation(FName("S_Rocket_Spawn"));
		FRotator Rotation = OwnerCharacter->GetActorForwardVector().ToOrientationRotator();

		FTransform SpawnPoint = FTransform(Rotation, SpawnLocation);
		ARocketProjectile* Rocket = Cast<ARocketProjectile>(UGameplayStatics::BeginSpawningActorFromClass(this, RocketType, SpawnPoint));
		if (Rocket)
		{
			UGameplayStatics::FinishSpawningActor(Rocket, SpawnPoint);
		}

		if (MuzzleEffect != nullptr)
		{
			auto FireEffect = UGameplayStatics::SpawnEmitterAtLocation(OwnerCharacter->GetWorld(), MuzzleEffect, SpawnLocation, Rotation);
		}
	}
}
