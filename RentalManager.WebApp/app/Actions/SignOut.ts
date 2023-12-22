"use server";

import { signOut } from "@/auth.js";

export default async function signOutAction() {
	await signOut();
}
