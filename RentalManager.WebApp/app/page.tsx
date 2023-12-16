"use client";

import { OpenAPI, UserService } from "@/app/ApiClient/codegen";

OpenAPI.BASE = "http://localhost:8080";
OpenAPI.WITH_CREDENTIALS = true;
OpenAPI.CREDENTIALS = "include";

export default function Home() {
	function test(): void {
		UserService.postUserLogin({
			userName: "JohnKowalski",
			password: "123123",
			useCookies: true,
			useSessionCookies: true,
		}).then((result) => {
			console.log(result);
		});
	}

	function test2(): void {
		UserService.getUser({}).then((result) => {
			console.log(result);
		});
	}

	return (
		<main className="flex min-h-screen flex-col items-center justify-between p-24">
			<button onClick={test}>LOGIN</button>
			<button onClick={test2}>GET</button>
		</main>
	);
}
