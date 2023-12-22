"use server";
import { UserDto, UserService } from "@/app/lib/actions/apiClient/client";

export default async function GetCurrentUser(): Promise<UserDto | null> {
	try {
		return await UserService.getUserWhoAmI();
	} catch (error: any) {
		return null;
	}
}
