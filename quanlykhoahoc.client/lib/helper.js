'use client';

import { toast } from 'react-toastify';
import { useMemo } from 'react';
import { useRouter, useSearchParams } from 'next/navigation';

async function TryFetch(action) {
  try {
    await action();
  } catch (error) {
    if (error.response) {
      try {
        const errors = JSON.parse(error.response).errors;
        for (const title in errors) {
          toast.warning(...errors[title]);
        }
      } catch (e) {
        toast.error('Lỗi Gì Đó');
      }
    }

    throw error;
  }
}

export async function handleSubmit(action, message) {
  toast.promise(TryFetch(action), {
    success: message,
    error: 'Vui Lòng Kiểm Tra Lại Thông Tin',
    pending: 'Đang Tải ...',
  });
}

export function useQuery() {
  const searchParams = useSearchParams();

  const { filters, sorts, page, pageSize, commentId } = useMemo(() => {
    return Object.fromEntries(searchParams.entries());
  }, [searchParams]);

  const query = useMemo(() => {
    return {
      filters,
      sorts: sorts,
      page: page,
      pageSize: pageSize,
      commentId: commentId
    };
  }, [filters, sorts, page, pageSize, commentId]);

  return query;
}

export function useSort() {
  const router = useRouter();

  const handleSort = useMemo(() => {
    return (field, remove = null) => {
      const parsed = queryString.parse(window.location.search);
      let sorts = parsed.sorts ? parsed.sorts.split(',') : [];

      const index = sorts.findIndex((sort) => sort === field || sort === `-${field}`);

      if (index !== -1) {
        if (remove) {
          sorts.splice(index, 1);
        } else {
          sorts[index] = sorts[index].startsWith('-') ? sorts[index].substr(1) : `-${sorts[index]}`;
        }
      } else {
        sorts.push(`-${field}`);
      }

      const newSorts = sorts.join(',');
      const newQuery = { ...parsed, sorts: newSorts || undefined };
      const newQueryString = queryString.stringify(newQuery);
      router.push(`?${newQueryString.replace(/%2C/g, ',')}`);
    };
  }, [router]);

  return handleSort;
}
