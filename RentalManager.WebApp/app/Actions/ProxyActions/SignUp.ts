"use server";

import { OpenAPI, UserService } from "@/app/lib/actions/apiClient/client";

OpenAPI.BASE = "http://localhost:8080";
OpenAPI.WITH_CREDENTIALS = true;
OpenAPI.CREDENTIALS = "include";

export default async function SignUp({
	formData,
}: {
	formData?: {
		Password: string;
		UserName: string;
		Name: string;
		Surname: string;
		Image?: Blob;
	};
}) {
	try {
		await UserService.postUser({
			formData: formData,
		});
		// return {
		// 	isSuccess: true,
		// 	title: null,
		// 	message: null,
		// };
	} catch (e: any) {
		console.log(e);
		// if (e.body?.detail?.includes("DuplicateUserName")) {
		// 	return {
		// 		isSuccess: false,
		// 		title: "Username already taken :/",
		// 		message: "Choose a different one.",
		// 	};
		// }
		//
		// return {
		// 	isSuccess: false,
		// 	title: e.body?.title,
		// 	message: e.body?.detail,
		// };
	}
}
