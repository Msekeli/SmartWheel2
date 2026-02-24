const BASE_URL =
  "https://smartwheel-api-msekeli-g0g3e5gvetb5fwdt.southafricanorth-01.azurewebsites.net";

export async function resolveIdentity(email) {
  const response = await fetch(`${BASE_URL}/identity`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({ email }),
  });

  if (!response.ok) {
    throw new Error("Identity resolution failed");
  }

  return await response.json();
}
