// Copyright 1998-2016 Epic Games, Inc. All Rights Reserved.
#pragma once
#include "GameFramework/Character.h"
#include "Weapons/ShooterWeaponComponent.h"
#include "UShooterCharacter.generated.h"

UCLASS(config=Game)
class AUShooterCharacter : public ACharacter
{
	GENERATED_BODY()

	/** Side view camera */
	UPROPERTY(VisibleAnywhere, BlueprintReadOnly, Category = Camera, meta = (AllowPrivateAccess = "true"))
	class UCameraComponent* SideViewCameraComponent;

	/** Camera boom positioning the camera beside the character */
	UPROPERTY(VisibleAnywhere, BlueprintReadOnly, Category = Camera, meta = (AllowPrivateAccess = "true"))
	class USpringArmComponent* CameraBoom;

	/** Begin play */
	virtual void BeginPlay() override;

public:

	// Health of the player
	UPROPERTY(EditDefaultsOnly, BlueprintReadOnly, Category = "Character")
	float Health;

	// Current number of shots made (used for animation)
	UPROPERTY(EditDefaultsOnly, BlueprintReadOnly, Category = "Animation")
	int32 AnimShotCount;

protected:

	/** Called for side to side input */
	void MoveRight(float Val);

	/** Handle touch inputs. */
	void TouchStarted(const ETouchIndex::Type FingerIndex, const FVector Location);

	/** Handle touch stop event. */
	void TouchStopped(const ETouchIndex::Type FingerIndex, const FVector Location);

	/** Set weapon 1 */
	UFUNCTION()
	void SetWeapon1();

	/** Set weapon 2 */
	UFUNCTION()
	void SetWeapon2();

	/** Fire weapon */
	UFUNCTION()
	void Fire();

	// APawn interface
	virtual void SetupPlayerInputComponent(class UInputComponent* InputComponent) override;
	// End of APawn interface

	/** Sets current weapon */
	void SetWeapon(EWeaponType Type);

public:
	AUShooterCharacter();

	/** Returns SideViewCameraComponent subobject **/
	FORCEINLINE class UCameraComponent* GetSideViewCameraComponent() const { return SideViewCameraComponent; }
	/** Returns CameraBoom subobject **/
	FORCEINLINE class USpringArmComponent* GetCameraBoom() const { return CameraBoom; }

	/** Current weapon (runtime only) */
	UPROPERTY(Transient, BlueprintReadOnly, Category = "Weapon")
	class UShooterWeaponComponent* CurrentWeapon;
};
