# AI Usage & Engineering Decisions

## 1. What did AI generate for you, and what did you write or modify yourself?

I used AI tools to help with parts of the backend, React + TypeScript frontend, routing, JWT authentication, API services, UI components, styling, debugging, and documentation.

I reviewed and tested the generated code and made the final decisions about the architecture, API behavior, authentication, frontend structure, and user experience. I also refactored the frontend into separate pages and reusable components instead of keeping everything in `App.tsx`.

## 2. What security issues did you find (or introduce) in the AI-generated code? How did you handle them?

* Passwords are stored as PBKDF2-HMAC-SHA256 hashes with a unique salt, not as plaintext.
* JWTs are signed and validated for issuer, audience, lifetime, and signing key. Protected actions are enforced by the backend using `[Authorize]`.
* The frontend stores the JWT in `localStorage` and sends it as a Bearer token. I am aware that `localStorage` can be accessed by injected JavaScript, so this is an assignment-level choice rather than the preferred approach for a higher-security production application.
* CORS is restricted to the frontend origin during local development.
* The development JWT key is not a production secret and should be replaced through secure deployment configuration.
* Request validation is handled through DTO validation attributes and ASP.NET Core model validation.

## 3. One thing the AI got wrong is that you had to fix. What was it and why was it wrong?

The AI initially made a few architectural mistakes that I had to fix:

* The CORS policy was registered, but `app.UseCors("Frontend")` was missing from the ASP.NET Core request pipeline. This caused the browser to reject frontend API requests. I identified the issue from the browser's CORS error and added the missing middleware.

* Some backend files had too many responsibilities and did not follow the Single Responsibility Principle. I refactored them so that each service/repository has a clearer and more focused responsibility.

* The frontend initially had too much code inside `App.tsx`. I refactored it into separate pages and reusable components to make the structure easier to maintain.

