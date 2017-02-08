// Fill out your copyright notice in the Description page of Project Settings.

#include "UShooter.h"
#include "ShooterWeaponComponent.h"

UShooterWeaponComponent::UShooterWeaponComponent()
{
	bWantsBeginPlay = false;
	bWantsInitializeComponent = false;
	PrimaryComponentTick.bCanEverTick = true;

	WeaponType = EWeaponType::Default;
	MuzzleEffect = nullptr;

	CurrentAmmoCount = 0;
	WeaponDamage = 50.0f;
}

void UShooterWeaponComponent::TickComponent( float DeltaTime, ELevelTick TickType, FActorComponentTickFunction* ThisTickFunction )
{
	Super::TickComponent( DeltaTime, TickType, ThisTickFunction );

}

void UShooterWeaponComponent::Fire()
{
	CurrentAmmoCount--;
}
