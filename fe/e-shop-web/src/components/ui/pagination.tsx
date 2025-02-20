import classes from './pagination.module.css';

interface PaginationProps {
    totalPages: number;
    currentPage: number;
    onPageChange: (page: number) => void;
}

export default function Pagination({totalPages, currentPage, onPageChange}: PaginationProps) {
    return (
        <div className={classes.container}>
          <button onClick={() => onPageChange(currentPage - 1)} disabled={currentPage === 1}>
            Previous
          </button>
    
          {/* Page Number Buttons */}
          {Array.from({ length: totalPages }, (_, i) => i + 1).map((pageNumber) => (
            <button
              key={pageNumber}
              onClick={() => onPageChange(pageNumber)}
              style={{
                fontWeight: currentPage === pageNumber ? "bold" : "normal",
                textDecoration: currentPage === pageNumber ? "underline" : "none",
              }}
            >
              {pageNumber}
            </button>
          ))}
    
          <button onClick={() => onPageChange(currentPage + 1)} disabled={currentPage === totalPages}>
            Next
          </button>
        </div>
      );
}