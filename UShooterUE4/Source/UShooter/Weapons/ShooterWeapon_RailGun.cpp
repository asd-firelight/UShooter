// Fill out your copyright notice in the Description page of Project Settings.

#include "UShooter.h"
#include "ShooterWeapon_RailGun.h"
#include "RocketProjectile.h"
#include "UShooterCharacter.h"

UShooterWeapon_RailGun::UShooterWeapon_RailGun()
{
	RailTrailEffect = nullptr;

	WeaponType = EWeaponType::RailGun;
}

void UShooterWeapon_RailGun::TickComponent( float DeltaTime, ELevelTick TickType, FActorComponentTickFunction* ThisTickFunction )
{
	Super::TickComponent( DeltaTime, TickType, ThisTickFunction );

}

void UShooterWeapon_RailGun::Fire()
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
		FVector EndPoint = SpawnLocation + OwnerCharacter->GetActorForwardVector() * 1000.0f;

		auto RailEffect = UGameplayStatics::SpawnEmitterAtLocation(OwnerCharacter->GetWorld(), RailTrailEffect, SpawnLocation);
		if (RailEffect)
		{
			RailEffect->SetBeamSourcePoint(0, SpawnLocation, 0);
			RailEffect->SetBeamEndPoint(0, EndPoint);
		}

		TArray<FHitResult> Hits;
		GetWorld()->LineTraceMultiByChannel(Hits, SpawnLocation, EndPoint, ECC_WEAPON);
		for (auto Hit: Hits)
		{
			if (Hit.GetActor() != nullptr)
			{
				FDamageEvent DmgEvent;
				Hit.GetActor()->TakeDamage(WeaponDamage, DmgEvent, nullptr, nullptr);
			}
		}
	}
}
