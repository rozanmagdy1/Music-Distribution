interface PaginationProps {
  page: number
  totalPages: number
  onPageChange: (page: number) => void
}

export function Pagination({ page, totalPages, onPageChange }: PaginationProps) {
  if (totalPages <= 1) return null

  return <nav className="pagination" aria-label="List pages">
    <button className="button button-secondary" type="button" disabled={page <= 1} onClick={() => onPageChange(page - 1)}>Previous</button>
    <span className="pagination-page">Page {page} of {totalPages}</span>
    <button className="button button-secondary" type="button" disabled={page >= totalPages} onClick={() => onPageChange(page + 1)}>Next</button>
  </nav>
}
