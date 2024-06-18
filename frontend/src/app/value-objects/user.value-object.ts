export class User {
    private constructor(
        public readonly id: number,
        public readonly username: string,
        public readonly email: string,
        public readonly avatarUrl: string
    ) {
    }

    public static create(values: any) {
        const requiredKeys = [
            "id",
            "username",
            "email",
            "avatar_url",
        ];

        requiredKeys.forEach(key => {
            if (!values.hasOwnProperty(key)) {
                throw new Error(`Could not create User object because required key "${key}" was missing`)
            }
        })

        return new User(
            values.id,
            values.username,
            values.email,
            values.avatar_url
        );
    }
}