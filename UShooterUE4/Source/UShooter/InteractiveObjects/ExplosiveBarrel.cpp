// Fill out your copyright notice in the Description page of Project Settings.

#include "UShooter.h"
#include "ExplosiveBarrel.h"


// Sets default values
AExplosiveBarrel::AExplosiveBarrel()
{
	Health = 20.0f;
}

void AExplosiveBarrel::OnDestroyed()
{
	Super::OnDestroyed();
}
