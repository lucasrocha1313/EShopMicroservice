"use client";

import { useTranslations } from 'next-intl';
import classes from './pagination.module.css';

interface PaginationProps {
    totalPages: number;
    currentPage: number;
    onPageChange: (page: number) => void;
}

export default function Pagination({totalPages, currentPage, onPageChange}: PaginationProps) {
    const t = useTranslations('Pagination');

    return (
        <div className={classes.container}>
          <button onClick={() => onPageChange(currentPage - 1)} disabled={currentPage === 1}>
            {t('previous')}
          </button>
    
          {/* Page Number Buttons */}
          {Array.from({ length: totalPages }, (_, i) => i + 1).map((pageNumber) => (
            <button
              key={pageNumber}
              onClick={() => onPageChange(pageNumber)}
              style={{
                fontWeight: currentPage === pageNumber ? "bold" : "normal",
                textDecoration: currentPage === pageNumber ? "underline" : "none",
                backgroundColor: currentPage === pageNumber ? "#0f0c47" : undefined,
              }}
            >
              {pageNumber}
            </button>
          ))}
    
          <button onClick={() => onPageChange(currentPage + 1)} disabled={currentPage === totalPages}>
            {t('next')}
          </button>
        </div>
      );
}