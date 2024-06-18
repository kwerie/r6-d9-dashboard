export class LoginSession {
    private constructor(
        public readonly access_token: string,
        public readonly refresh_token: string,
        public readonly refresh_token_expires_at: Date
    ) {
    }

    public static create(data: any): LoginSession {
        const requiredKeys = [
            "access_token",
            "refresh_token",
            "refresh_token_expires_at"
        ];

        requiredKeys.forEach(key => {
            if (!data.hasOwnProperty(key)) {
                throw new Error(`Could not create User object because required key "${key}" was missing`)
            }
        })

        return new LoginSession(
            data["access_token"],
            data["refresh_token"],
            data["refresh_token_expires_at"]
        )
    }

    public static createFromJSON(data: string): LoginSession {
        return LoginSession.create(JSON.parse(data));
    }
}