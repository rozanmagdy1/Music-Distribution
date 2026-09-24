export function LoadingState({ label = 'Loading…' }: { label?: string }) {
  return <div className="feedback"><span className="spinner" aria-hidden="true" />{label}</div>
}

export function ErrorMessage({ children }: { children: string }) {
  return <div className="alert alert-error" role="alert">{children}</div>
}
